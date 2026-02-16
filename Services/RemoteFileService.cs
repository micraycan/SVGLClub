using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SVGLClub.Services
{
    public class RemoteFileService : IRemoteFileService
    {
        private readonly HttpClient _client;
        private readonly IAPISessionProvider _apiSessionProvider;
        private readonly IConfiguration _config;


        public RemoteFileService(IAPISessionProvider apiSessionProvider, HttpClient client, IConfiguration config)
        {
            _apiSessionProvider = apiSessionProvider;
            _client = client;
            _config = config;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("SVGL/2.0");
        }

        public async Task<string> DownloadFileAsync(string subDir, string filename)
        {
            string sessionId = await _apiSessionProvider.GetSessionIDAsync();
            string remotePath = Path.Combine(_config["AMPServer:Dir"]!, subDir, filename);

            StringBuilder sb = new();
            long offset = 0;

            while (true)
            {
                string body = JsonSerializer.Serialize(new
                {
                    Filename = remotePath,
                    Offset = offset,
                    SESSIONID = sessionId
                });

                HttpResponseMessage rs = await _client.PostAsync(
                    $"{_config["AMPServer:APIBase"]}/FileManagerPlugin/ReadFileChunk",
                    new StringContent(body, Encoding.UTF8, "application/json")
                );

                rs.EnsureSuccessStatusCode();

                string payload = await rs.Content.ReadAsStringAsync();
                JsonElement root = JsonSerializer.Deserialize<JsonElement>(payload);

                if (!root.TryGetProperty("Result", out JsonElement resultElem))
                {
                    break;
                }

                string chunk = resultElem.GetString()!;

                if (string.IsNullOrEmpty(chunk)) { break; }

                byte[] bytes = Convert.FromBase64String(chunk);
                sb.Append(Encoding.UTF8.GetString(bytes));
                offset += bytes.Length;
            }

            return sb.ToString();
        }

        public async Task<List<string>> GetSessionFilesAsync()
        {
            string sessionId = await _apiSessionProvider.GetSessionIDAsync();

            string body = JsonSerializer.Serialize(new
            {
                Dir = _config["AMPServer:ResultDir"],
                SESSIONID = sessionId
            });

            HttpResponseMessage rs = await _client.PostAsync(
                $"{_config["AMPServer:APIBase"]}/FileManagerPlugin/GetDirectoryListing",
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            rs.EnsureSuccessStatusCode();

            string text = await rs.Content.ReadAsStringAsync();
            JsonElement arr = JsonSerializer.Deserialize<JsonElement>(text);

            List<string> list = new();

            if (arr.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement item in arr.EnumerateArray())
                {
                    string fn = item.GetProperty("Filename").GetString()!;
                    if (!string.IsNullOrEmpty(fn) && fn.EndsWith(".json"))
                    {
                        list.Add(fn);
                    }
                }
            }

            return list;
        }
    }

    public interface IRemoteFileService
    {
        Task<List<string>> GetSessionFilesAsync();
        Task<string> DownloadFileAsync(string subDir, string filename);
    }
}
