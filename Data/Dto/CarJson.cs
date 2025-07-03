namespace SVGLClub.Data.Dto
{
    public class CarJson
    {
        public int CarId { get; set; }
        public DriverJson Driver { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Skin { get; set; } = null!;
        public int BallastKG { get; set; }
        public int Restrictor { get; set; }
    }
}
