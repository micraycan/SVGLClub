using Microsoft.EntityFrameworkCore;

namespace SVGLClub.Data.Services
{
    public class QueryService : IQueryService
    {
        private readonly ApplicationDbContext _db;

        public QueryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<List<AssettoSession>> QuerySessions()
        {
            var q = _db!.Sessions.AsQueryable();

            return q.Include(s => s.Cars)
                .Include(s => s.Results)
                .Include(s => s.Laps)
                .Include(s => s.Events)
                .AsSplitQuery()
                .ToListAsync();
        }

        public Task<List<SessionResult>> QueryResultsAsync(int sessionId)
        {
            var q = _db!.SessionResult.AsQueryable();

            return q
                .Where(r => r.AssettoSessionId == sessionId
                && r.BestLap != 999999999
                && r.TotalTime != 0)
                .ToListAsync();
        }

        public Task<List<SessionLap>> QueryLapsAsync(int sessionId, string driverGuid, string car)
        {
            var q = _db!.SessionLaps.AsQueryable();

            return q.Where(l => l.DriverGuid == driverGuid
                && l.CarModel == car
                && l.AssettoSessionId == sessionId)
                .ToListAsync();
        }

        public Task<string?> GetFavoriteCarByDriverAsync(string driverGuid)
        {
            var q = _db!.SessionLaps.AsQueryable();

            return q.Where(l => l.DriverGuid == driverGuid)
                .GroupBy(l => l.CarModel)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetFavoriteTrackByDriverAsync(string driverGuid)
        {
            var q = _db!.SessionLaps.AsQueryable();

            return await _db.SessionLaps
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

        public Task<int> GetLapCountByDriverAsync(string driverGuid)
        {
            var q = _db!.SessionLaps.AsQueryable();

            return q.CountAsync(l => l.DriverGuid == driverGuid);
        }
    }
}
