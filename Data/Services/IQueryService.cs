namespace SVGLClub.Data.Services
{
    public interface IQueryService
    {
        Task<List<AssettoSession>> QuerySessions();

        Task<List<SessionResult>> QueryResultsAsync(int sessionId);

        Task<List<SessionLap>> QueryLapsAsync(int sessionId, string driverGuid, string car);

        Task<string?> GetFavoriteCarByDriverAsync(string driverGuid);
        Task<int> GetLapCountByDriverAsync(string driverGuid);
        Task<string?> GetFavoriteTrackByDriverAsync(string driverGuid);
    }
}
