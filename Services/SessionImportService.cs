using SVGLClub.Data;
using SVGLClub.Data.DTO;

namespace SVGLClub.Services
{
    public class SessionImportService : ISessionImportService
    {
        private readonly IRemoteFileService _fileService;
        private readonly ISessionDBSaver _sessionSaver;
        private readonly ISessionJsonDeserializer _deserializer;
        private readonly ISessionMapper _sessionMapper;
        private readonly IConfiguration _config;

        public SessionImportService(
            IRemoteFileService fileService,
            ISessionDBSaver sessionSaver,
            ISessionJsonDeserializer deserializer,
            ISessionMapper sessionMapper,
            IConfiguration config)
        {
            _fileService = fileService;
            _sessionSaver = sessionSaver;
            _deserializer = deserializer;
            _sessionMapper = sessionMapper;
            _config = config;
        }

        public async Task<bool> ImportNewSessionsAsync()
        {
            List<string> remoteFiles = await _fileService.GetSessionFilesAsync();
            List<string> existing = await _sessionSaver.GetExistingFilenameAsync();
            List<string> toImport = remoteFiles.Except(existing).ToList();

            if (!toImport.Any()) { return false; }

            foreach (string filename in toImport)
            {
                try
                {
                    string rawJson = await _fileService.DownloadFileAsync(_config["AMPServer:ResultDir"]!, filename);
                    SessionJson dto = _deserializer.Deserialize(rawJson);

                    if (dto == null) { continue; }

                    AssettoSession session = _sessionMapper.Map(dto, filename);

                    await _sessionSaver.AddAsync(session);
                }
                catch
                {
                    continue;
                }
            }

            await _sessionSaver.SaveAsync();
            return true;
        }
    }

    public interface ISessionImportService
    {
        Task<bool> ImportNewSessionsAsync();
    }
}
