using System.Text.Json;
using System.Globalization;

namespace SVGLClub.Data.Services
{
    public class ContentLoader : IContentLoader
    {
        private readonly string _contentFolder;
        private readonly IQueryService _query;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true };
        Dictionary<string, string> _contentNames = new();

        public ContentLoader(IWebHostEnvironment env, IQueryService query)
        {
            _contentFolder = Path.Combine(env.WebRootPath!, "accontent");
            _query = query;
        }

        public string GetDisplayName(string key)
        {
            return _contentNames[key];
        }

        public async Task GetNamesAsync(List<AssettoSession> sessions)
        {
            Dictionary<string, string> formattedNames = new();

            foreach (var session in sessions)
            {
                var fulltrack = session.TrackName + "|" + session.TrackConfig;

                foreach (var lap in session.Laps!)
                {
                    if (formattedNames.TryGetValue(lap.CarModel, out _)) { continue; }
                    formattedNames[lap.CarModel] = await GetCarNameAsync(lap.CarModel);
                }

                if (formattedNames.TryGetValue(fulltrack, out _)) { continue; }
                formattedNames[fulltrack] = await GetTrackNameAsync(session!.TrackName, session.TrackConfig);
            }

            _contentNames = formattedNames;
        }

        private async Task<string> GetCarNameAsync(string car)
        {
            if (string.IsNullOrEmpty(car)) { return "error"; }
            var path = Path.Combine(_contentFolder, "cars", car, "ui_car.json");

            if (!File.Exists(path)) { return car + "___"; }

            var data = await File.ReadAllTextAsync(path);
            var name = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("name").ToString() ?? car + "___";

            return name;
        }

        private async Task<string> GetTrackNameAsync(string track, string config)
        {
            if (string.IsNullOrEmpty(track)) { return "error"; }
            var trackLayout = config != string.Empty ? config : "default";
            var path = Path.Combine(_contentFolder, "tracks", track, trackLayout, "ui_track.json");

            if (!File.Exists(path)) { return track + "___"; }

            var data = await File.ReadAllTextAsync(path);

            var name = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("name").ToString() ?? track + "___";

            return name;
        }

        public async Task<List<TrackSectionPoint>> GetTrackSectionPointsAsync(string trackId)
        {
            var decodedTrack = trackId.Split('|');
            var track = decodedTrack[0];
            var config = !string.IsNullOrEmpty(decodedTrack[1]) ? decodedTrack[1] : "default";
            var path = Path.Combine(_contentFolder, "tracks", track, config, "sections.ini");

            if (!File.Exists(path)) { return  new List<TrackSectionPoint>(); }

            var data = await File.ReadAllTextAsync(path);
            var lines = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .ToList();

            var sections = new List<TrackSectionPoint>();
            TrackSectionPoint? current = null;

            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw) || raw.StartsWith(";") || raw.StartsWith("#")) { continue; }

                if (raw.StartsWith("[") && raw.EndsWith("]"))
                {
                    if (current != null) { sections.Add(current); }

                    current = new TrackSectionPoint();
                    continue;
                }

                var parts = raw.Split('=', 2);
                if (parts.Length != 2 || current == null) { continue; }
                var key = parts[0].Trim().ToUpperInvariant();
                var val = parts[1].Trim();

                switch (key)
                {
                    case "IN":
                        if (double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out var inPos))
                        {
                            current.In = inPos;
                        
                        }
                        break;

                    case "OUT":
                        if (double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out var outPos))
                        {
                            current.Out = outPos;
                        }
                        break;

                    case "TEXT":
                        current.Label = val;
                        break;

                    default:
                        break;
                }
            }

            return sections;
        }
    }
}
