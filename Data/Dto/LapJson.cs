namespace SVGLClub.Data.DTO
{
    public class LapJson
    {
        public string DriverName { get; set; } = null!;
        public string DriverGuid { get; set; } = null!;
        public int CarId { get; set; }
        public string CarModel { get; set; } = null!;
        public long Timestamp { get; set; }
        public int LapTime { get; set; }
        public long[] Sectors { get; set; } = null!;
        public int Cuts { get; set; }
        public string Tyre { get; set; } = null!;
        public int BallastKG { get; set; }
        public int Restrictor { get; set; }
    }
}
