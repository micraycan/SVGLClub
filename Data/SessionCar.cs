namespace SVGLClub.Data
{
    public class SessionCar
    {
        public int Id { get; set; }
        public int AssettoSessionId { get; set; }
        public AssettoSession AssettoSession { get; set; } = null!;
        public int CarId { get; set; }
        public string DriverName { get; set; } = null!;
        public string DriverGuid { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Skin { get; set; } = null!;
        public int BallastKG { get; set; }
        public int Restrictor { get; set; }
    }
}
