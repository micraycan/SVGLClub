using Microsoft.EntityFrameworkCore;
using SVGLClub.Data;

namespace SVGLClub.Services
{
    public class SessionDBSaver : ISessionDBSaver
    {
        private readonly AppDBContext _db;

        public SessionDBSaver(AppDBContext db) => _db = db;

        public async Task AddAsync(AssettoSession session) => await _db.Sessions.AddAsync(session);

        public Task<bool> ExistsAsync(string filename) => _db.Sessions.AnyAsync(s => s.Filename == filename);

        public Task<List<string>> GetExistingFilenameAsync() => _db.Sessions.Select(s => s.Filename).ToListAsync();

        public Task SaveAsync() => _db.SaveChangesAsync();
    }

    public interface ISessionDBSaver
    {
        Task<bool> ExistsAsync(string filename);
        Task<List<string>> GetExistingFilenameAsync();
        Task AddAsync(AssettoSession session);
        Task SaveAsync();
    }
}
