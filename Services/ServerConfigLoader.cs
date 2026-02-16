using SVGLClub.Data;
using System.Text.RegularExpressions;

namespace SVGLClub.Services
{
    public class ServerConfigLoader : IServerConfigLoader
    {
        private readonly IRemoteFileService _fileService;
        private readonly IConfiguration _config;

        public ServerConfigLoader(IRemoteFileService fileService, IConfiguration config)
        {
            _fileService = fileService;
            _config = config;
        }

        public async Task<ServerConfig> LoadServerConfigAsync()
        {
            string configString = await _fileService.DownloadFileAsync(_config["AMPServer:ConfigDir"]!, _config["AMPServer:ConfigFile"]!);

            Match carMatch = Regex.Match(configString, @"^CARS=(?<cars>[^\r\n]+)", RegexOptions.Multiline);
            List<string> cars = new();

            if (carMatch.Success)
            {
                cars = carMatch.Groups["cars"].Value
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

            Match trackConfigMatch = Regex.Match(configString, @"^CONFIG_TRACK=(?<value>[^\r\n]+)", RegexOptions.Multiline);
            string trackConfig = trackConfigMatch.Success ? trackConfigMatch.Groups["value"].Value : string.Empty;

            Match trackMatch = Regex.Match(configString, @"^TRACK=(?<value>[^\r\n]+)", RegexOptions.Multiline);
            string track = trackMatch.Success ? trackMatch.Groups["value"].Value : string.Empty;

            return new ServerConfig()
            {
                Cars = cars,
                TrackConfig = trackConfig,
                TrackName = track
            };
        }
    }

    public interface IServerConfigLoader
    {
        Task<ServerConfig> LoadServerConfigAsync();
    }
}
