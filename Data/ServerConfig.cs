namespace SVGLClub.Data
{
    public class ServerConfig
    {
        public string TrackName { get; set; } = string.Empty;
        public string TrackConfig { get; set; } = string.Empty;
        public List<string> Cars { get; set; } = new();
    }
}
