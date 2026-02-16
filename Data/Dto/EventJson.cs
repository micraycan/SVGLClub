namespace SVGLClub.Data.DTO
{
    public class EventJson
    {
        public string Type { get; set; } = null!;
        public int CarId { get; set; }
        public DriverJson Driver { get; set; } = null!;
        public int OtherCarId { get; set; }
        public DriverJson OtherDriver { get; set; } = null!;
        public double ImpactSpeed { get; set; }
    }
}
