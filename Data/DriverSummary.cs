namespace SVGLClub.Data
{
    public class DriverSummary
    {
        public string DriverGuid { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public int LapCount { get; set; }
        public string FavoriteCar { get; set; } = string.Empty;
        public string FavoriteTrack { get; set; } = string.Empty;
        public HashSet<string> TracksDriven { get; set; } = new();
        public Dictionary<string, List<SessionLap>> LapsByTrack { get; set; } = new();
    }
}
