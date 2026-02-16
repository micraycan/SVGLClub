namespace SVGLClub.Data.DTO
{
    public class ResultJson
    {
        public string DriverName { get; set; } = null!;
        public string DriverGuid { get; set; } = null!;
        public int CarId { get; set; }
        public string CarModel { get; set; } = null!;
        public long BestLap { get; set; }
        public long TotalTime { get; set; }
        public int BallastKG { get; set; }
        public int Restrictor { get; set; }
    }
}
