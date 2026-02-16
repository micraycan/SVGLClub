namespace SVGLClub.Data.DTO
{
    public class DriverJson
    {
        public string Name { get; set; } = null!;
        public string Team { get; set; } = null!;
        public string Nation { get; set; } = null!;
        public string Guid { get; set; } = null!;
        public List<string>? GuidsList { get; set; }
    }
}
