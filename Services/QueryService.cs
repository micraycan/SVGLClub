using Microsoft.EntityFrameworkCore;
using SVGLClub.Data;

namespace SVGLClub.Services
{
    public class QueryService : IQueryService
    {
        private readonly AppDBContext _db;

        public QueryService(AppDBContext db) => _db = db;

        public Task<List<AssettoSession>> GetAllSessionsAsync()
        {
            return _db!.Sessions.AsQueryable()
                .Include(s => s.Cars)
                .Include(s => s.Results)
                .Include(s => s.Laps)
                .Include(s => s.Events)
                .AsSplitQuery()
                .ToListAsync();
        }

        public Task<AssettoSession?> GetSessionAsync(int id)
        {
            return _db!.Sessions.AsQueryable()
                .Include(s => s.Cars)
                .Include(s => s.Results)
                .Include(s => s.Laps)
                .Include(s => s.Events)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<SessionResult>> GetAllResultsAsync(int sessionId)
        {
            return _db!.SessionResult.AsQueryable()
                .Where(r => r.AssettoSessionId == sessionId
                && r.BestLap != 999999999
                && r.TotalTime != 0)
                .ToListAsync();
        }

        public Task<List<SessionLap>> GetLapsAsync(int sessionId, string driverGuid, string car)
        {
            return _db!.SessionLaps.AsQueryable()
                .Where(l => l.DriverGuid == driverGuid
                && l.CarModel == car
                && l.AssettoSessionId == sessionId)
                .ToListAsync();
        }

        public Task<int> GetDriverLapCountAsync(string driverGuid)
        {
            return _db!.SessionLaps.AsQueryable()
                .CountAsync(l => l.DriverGuid == driverGuid);
        }
        
        public Task<string?> GetDriverFavoriteCarAsync(string driverGuid)
        {
            return _db!.SessionLaps.AsQueryable()
                .Where(l => l.DriverGuid == driverGuid)
                .GroupBy(l => l.CarModel)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();
        }

        public Task<string?> GetDriverFavoriteTrackAsync(string driverGuid)
        {
            return _db!.SessionLaps.AsQueryable()
                .Where(l => l.DriverGuid == driverGuid)
                .Join(_db.Sessions,
                    l => l.AssettoSessionId,
                    s => s.Id,
                    (l, s) => s.TrackName + "|" + s.TrackConfig)
                .GroupBy(t => t)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();
        }
    }

    public interface IQueryService
    {
        Task<List<AssettoSession>> GetAllSessionsAsync();
        Task<AssettoSession?> GetSessionAsync(int id);
        Task<List<SessionResult>> GetAllResultsAsync(int sessionId);
        Task<List<SessionLap>> GetLapsAsync(int sessionId, string driverGuid, string car);
        Task<int> GetDriverLapCountAsync(string driverGuid);
        Task<string?> GetDriverFavoriteCarAsync(string driverGuid);
        Task<string?> GetDriverFavoriteTrackAsync(string driverGuid);
    }
}
