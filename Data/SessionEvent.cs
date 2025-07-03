namespace SVGLClub.Data
{
    public class SessionEvent
    {
        public int Id { get; set; }
        public int AssettoSessionId { get; set; }
        public AssettoSession AssettoSession { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int CarId { get; set; }
        public string DriverName { get; set; } = null!;
        public string DriverGuid { get; set; } = null!;
        public int OtherCarId { get; set; }
        public string OtherDrivername { get; set; } = null!;
        public double ImpactSpeed { get; set; }
    }
}
