namespace SVGLClub.Data
{
    public class SessionLap
    {
        public int Id { get; set; }
        public int AssettoSessionId { get; set; }
        public AssettoSession AssettoSession { get; set; } = null!;
        public string DriverName { get; set; } = null!;
        public string DriverGuid { get; set; } = null!;
        public int CarId { get; set; }
        public string CarModel { get; set; } = null!;
        public long Timestamp { get; set; }
        public int LapTime { get; set; }
        public int Sector1 { get; set; }
        public int Sector2 { get; set; }
        public int Sector3 { get; set; }
        public int Cuts { get; set; }
        public string Tyre { get; set; } = null!;
        public int BallastKG { get; set; }
        public int Restrictor { get; set; }
    }
}
