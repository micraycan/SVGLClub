using Microsoft.EntityFrameworkCore;
using SVGLClub.Data.Dto;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SVGLClub.Data.Services
{
    public class SessionSyncService : ISessionSyncService
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _client;
        private readonly ILogger<SessionSyncService> _logger;

        private string _apiBase = Environment.GetEnvironmentVariable("SVGL_API_BASE") ?? throw new InvalidOperationException("SVGL_API_BASE not set");
        private string _userName = Environment.GetEnvironmentVariable("SVGL_USER") ?? throw new InvalidOperationException("SVGL_USER not set");
        private string _password = Environment.GetEnvironmentVariable("SVGL_PASS") ?? throw new InvalidOperationException("SVGL_PASS not set");
        private string _resultDir = Environment.GetEnvironmentVariable("SVGL_RESULT_DIR") ?? throw new InvalidOperationException("SVGL_RESULT_DIR not set");
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
        };

        public SessionSyncService(HttpClient client, ApplicationDbContext db, ILogger<SessionSyncService> logger)
        {
            _client = client;
            _db = db;
            _logger = logger;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("SVGL/1.0");
        }

        public async Task UpdateSessionsAsync()
        {
            var remoteFiles = await GetRemoteSessionFilesAsync();
            var existing = await _db.Sessions.Select(s => s.Filename).ToListAsync();

            var toImport = remoteFiles.Except(existing).ToList();
            if(!toImport.Any())
            {
                _logger.LogInformation("No new sessions to import");
                return;
            }

            foreach (var filename in toImport)
            {
                if (await _db.Sessions.AnyAsync(s => s.Filename == filename))
                {
                    _logger.LogWarning($"Skipping duplicate session {filename}");
                    continue;
                }

                try
                {
                    var rawJson = await DownloadSessionJsonAsync(filename);
                    var dto = JsonSerializer.Deserialize<SessionJsonModel>(rawJson, _jsonOptions);
                    if (dto == null)
                    {
                        _logger.LogWarning("Failed to parse JSON for {File}", filename);
                        continue;
                    }

                    var session = new AssettoSession
                    {
                        Filename = filename,
                        TrackName = dto.TrackName,
                        TrackConfig = dto.TrackConfig,
                        SessionType = dto.Type,
                        DurationSecs = dto.DurationSecs,
                        RaceLaps = dto.RaceLaps
                    };

                    session.Cars = dto.Cars?.Select(c => new SessionCar
                    {
                        CarId = c.CarId,
                        DriverName = c.Driver.Name,
                        DriverGuid = c.Driver.Guid,
                        Model = c.Model,
                        Skin = c.Skin,
                        BallastKG = c.BallastKG,
                        Restrictor = c.Restrictor
                    }).ToList() ?? new List<SessionCar>();

                    session.Results = dto.Result?.Select(r => new SessionResult
                    {
                        DriverName = r.DriverName,
                        DriverGuid = r.DriverGuid,
                        CarId = r.CarId,
                        CarModel = r.CarModel,
                        BestLap = r.BestLap,
                        TotalTime = r.TotalTime,
                        BallastKG = r.BallastKG,
                        Restrictor = r.Restrictor
                    }).ToList() ?? new List<SessionResult>();

                    session.Laps = dto.Laps?.Select(l => 
                    {
                        var sectors = l.Sectors ?? Array.Empty<int>();
                        return new SessionLap
                        {
                            DriverName = l.DriverName,
                            DriverGuid = l.DriverGuid,
                            CarId = l.CarId,
                            CarModel = l.CarModel,
                            Timestamp = l.Timestamp,
                            LapTime = l.LapTime,
                            Sector1 = sectors.Length > 0 ? sectors[0] : 0,
                            Sector2 = sectors.Length > 1 ? sectors[1] : 0,
                            Sector3 = sectors.Length > 2 ? sectors[2] : 0,
                            Cuts = l.Cuts,
                            Tyre = l.Tyre,
                            BallastKG = l.BallastKG,
                            Restrictor = l.Restrictor
                        };
                    }).ToList() ?? new List<SessionLap>();

                    session.Events = dto.Events?.Select(e => new SessionEvent
                    {
                        Type = e.Type,
                        CarId = e.CarId,
                        DriverName = e.Driver.Name,
                        DriverGuid = e.Driver.Guid,
                        OtherCarId = e.OtherCarId,
                        OtherDrivername = e.OtherDriver.Name,
                        ImpactSpeed = e.ImpactSpeed,
                    }).ToList() ?? new List<SessionEvent>();

                    _db.Sessions.Add(session);
                    try
                    {
                        await _db.SaveChangesAsync();
                        _logger.LogInformation("Imported session {file}", filename);
                    }
                    catch(DbUpdateException ex)
                    {
                        _logger.LogWarning($"Duplicate detected on save, skipping {filename} : {ex}");
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error import session {file}", filename);
                }
            }
        }

        private async Task<string> DownloadSessionJsonAsync(string filename)
        {
            var sessionId = await GetApiSessionIdAsync();
            var remotePath = Path.Combine(_resultDir, filename);

            var sb = new StringBuilder();
            long offset = 0;

            while (true)
            {
                var body = JsonSerializer.Serialize(new
                {
                    Filename = remotePath,
                    Offset = offset,
                    SESSIONID = sessionId
                });

                var rs = await _client.PostAsync(
                    $"{_apiBase}/FileManagerPlugin/ReadFileChunk",
                    new StringContent(body, Encoding.UTF8, "application/json")
                );

                rs.EnsureSuccessStatusCode();

                var payload = await rs.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<JsonElement>(payload);
                
                if (!root.TryGetProperty("Result", out var resultElem))
                {
                    _logger.LogWarning($"ReadFileChunk for {filename} returned unexpected JSON: {payload}");
                    break;
                }

                var chunk = resultElem.GetString();

                if (string.IsNullOrEmpty(chunk)) { break; }

                var bytes = Convert.FromBase64String(chunk);
                sb.Append(Encoding.UTF8.GetString(bytes));
                offset += bytes.Length;
            }

            return sb.ToString();
        }

        private async Task<string> GetApiSessionIdAsync()
        {
            var body = JsonSerializer.Serialize(new
            {
                username = _userName,
                password = _password,
                token = string.Empty,
                rememberMe = false
            });

            var rs = await _client.PostAsync(
                $"{_apiBase}/Core/Login",
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            rs.EnsureSuccessStatusCode();

            var text = await rs.Content.ReadAsStringAsync();
            var doc = JsonSerializer.Deserialize<JsonElement>(text);
            return doc.GetProperty("sessionID").GetString()!;
        }

        private async Task<List<string>> GetRemoteSessionFilesAsync()
        {
            var sessionId = await GetApiSessionIdAsync();
            var body = JsonSerializer.Serialize(new
            {
                Dir = _resultDir,
                SESSIONID = sessionId
            });

            var rs = await _client.PostAsync(
                $"{_apiBase}/FileManagerPlugin/GetDirectoryListing",
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            rs.EnsureSuccessStatusCode();

            var text = await rs.Content.ReadAsStringAsync();
            var arr = JsonSerializer.Deserialize<JsonElement>(text);

            var list = new List<string>();
            if (arr.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in arr.EnumerateArray())
                {
                    var fn = item.GetProperty("Filename").GetString();
                    if (!string.IsNullOrEmpty(fn) && fn.EndsWith(".json"))
                    {
                        list.Add(fn);
                    }
                }
            }

            return list;
        }
    }
}
