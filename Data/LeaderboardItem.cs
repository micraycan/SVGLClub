namespace SVGLClub.Data
{
    public class LeaderboardItem
    {
        public string DriverGuid { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public long FastestLapMs { get; set; }
        public string CarModel { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}
