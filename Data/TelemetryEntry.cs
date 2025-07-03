namespace SVGLClub.Data
{
    public class TelemetryEntry
    { 
        public int Id { get; set; }
        public string Driver { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string TrackVariation { get; set; } = string.Empty;
        public string Car { get; set; } = string.Empty;
        public int LapTimeMs { get; set; }
        public int NumDataPoints { get; set; }

        public string PositionJson { get; set; } = string.Empty;
        public string GearJson { get; set; } = string.Empty;
        public string SpeedJson { get; set; } = string.Empty;
        public string ThrottleJson { get; set; } = string.Empty;
        public string BrakeJson { get; set; } = string.Empty;
    }
}
