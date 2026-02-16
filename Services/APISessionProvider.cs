using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SVGLClub.Services
{
    public class APISessionProvider : IAPISessionProvider
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public APISessionProvider(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("SVGL/2.0");
        }

        public async Task<string> GetSessionIDAsync()
        {
            var body = JsonSerializer.Serialize(new
            {
                username = _config["AMPServer:User"],
                password = _config["AMPServer:Pass"],
                token = string.Empty,
                rememberMe = false
            });

            var rs = await _client.PostAsync(
                $"{_config["AMPServer:APIBase"]}/Core/Login",
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            rs.EnsureSuccessStatusCode();

            var text = await rs.Content.ReadAsStringAsync();
            var doc = JsonSerializer.Deserialize<JsonElement>(text);
            return doc.GetProperty("sessionID").GetString()!;
        }
    }

    public interface IAPISessionProvider
    {
        Task<string> GetSessionIDAsync();
    }
}
