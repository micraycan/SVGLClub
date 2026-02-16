using SVGLClub.Data;
using SVGLClub.Utilities;

namespace SVGLClub.Services
{
    public class DriverStatService : IDriverStatService
    {
        public Dictionary<string, DriverSummary> BuildDriverSummaries(List<AssettoSession> sessions)
        {
            Dictionary<string, DriverSummary> summaries = new();

            Dictionary<int, string> sessionTrackLookup = sessions.ToDictionary(
                s => s.Id,
                s => Formatter.BuildTrackId(s.TrackName, s.TrackConfig)
            );

            foreach (AssettoSession session in sessions)
            {
                if (session.Laps == null) { continue; }

                foreach (SessionLap lap in session.Laps)
                {
                    if (!summaries.TryGetValue(lap.DriverGuid, out var summary))
                    {
                        summary = new DriverSummary()
                        {
                            DriverGuid = lap.DriverGuid,
                            DriverName = GetDriverName(sessions, lap.DriverGuid)
                        };

                        summaries.Add(lap.DriverGuid, summary);
                    }

                    summary.LapCount++;

                    string trackId = sessionTrackLookup[lap.AssettoSessionId];
                    summary.TracksDriven.Add(trackId);

                    if (!summary.LapsByTrack.ContainsKey(trackId))
                    {
                        summary.LapsByTrack[trackId] = new();
                    }

                    summary.LapsByTrack[trackId].Add(lap);
                }
            }

            foreach (DriverSummary summary in summaries.Values)
            {
                summary.FavoriteCar = summary
                    .LapsByTrack
                    .SelectMany(kv => kv.Value)
                    .GroupBy(l => l.CarModel)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault() ?? string.Empty;

                summary.FavoriteTrack = summary
                    .LapsByTrack
                    .OrderByDescending(kv => kv.Value.Count())
                    .Select(kv => kv.Key)
                    .FirstOrDefault() ?? string.Empty;
            }

            return summaries;
        }

        public string GetDriverName(List<AssettoSession> sessions, string driverGuid)
        {
            return sessions
                .SelectMany(s => s.Results ?? Enumerable.Empty<SessionResult>())
                .FirstOrDefault(r => r.DriverGuid == driverGuid)
                ?.DriverName ?? string.Empty;
        }
    }

    public interface IDriverStatService
    {
        Dictionary<string, DriverSummary> BuildDriverSummaries(List<AssettoSession> sessions);
        string GetDriverName(List<AssettoSession> sessions, string driverGuid);
    }
}
