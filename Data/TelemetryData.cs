namespace SVGLClub.Data
{
    public class TelemetryData
    {
        public string User { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string TrackVariation { get; set; } = string.Empty;
        public string Car { get; set; } = string.Empty;
        public int LapTimeMs { get; set; }
        public int NumDataPoints { get; set; }
        public List<float> Position { get; set; } = new();
        public List<int> Gear { get; set; } = new();
        public List<float> Speed { get; set; } = new();
        public List<float> Throttle { get; set; } = new();
        public List<float> Brake { get; set; } = new();
    }
}
