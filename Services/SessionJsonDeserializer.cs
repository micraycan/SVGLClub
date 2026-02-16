using SVGLClub.Data.DTO;
using System.Text.Json;

namespace SVGLClub.Services
{
    public class SessionJsonDeserializer : ISessionJsonDeserializer
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
        };

        public SessionJson Deserialize(string json) => JsonSerializer.Deserialize<SessionJson>(json, _jsonOptions);
    }

    public interface ISessionJsonDeserializer
    {
        SessionJson Deserialize(string json);
    }
}
