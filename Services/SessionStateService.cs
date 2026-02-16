using SVGLClub.Data;

namespace SVGLClub.Services
{
    public class SessionStateService : ISessionStateService
    {
        private readonly ISessionImportService _sessionImporter;
        private readonly IQueryService _queryService;
        private readonly IContentLoader _contentLoader;
        private readonly IServerConfigLoader _configLoader;

        public List<AssettoSession> Sessions { get; private set; } = new();
        public ServerConfig Config { get; private set; } = new();

        public SessionStateService(ISessionImportService sessionImporter, IQueryService queryService, IContentLoader contentLoader, IServerConfigLoader configLoader)
        {
            _sessionImporter = sessionImporter;
            _queryService = queryService;
            _contentLoader = contentLoader;
            _configLoader = configLoader;
        }

        public async Task EnsureLoadedAsync()
        {
            bool imported = await _sessionImporter.ImportNewSessionsAsync();
            Config = await _configLoader.LoadServerConfigAsync();

            if (imported || !Sessions.Any())
            {
                Sessions = await _queryService.GetAllSessionsAsync();
                await _contentLoader.LoadAllContentNamesAsync();
            }
        }
    }

    public interface ISessionStateService
    {
        List<AssettoSession> Sessions { get; }
        ServerConfig Config { get; }
        Task EnsureLoadedAsync();
    }
}
