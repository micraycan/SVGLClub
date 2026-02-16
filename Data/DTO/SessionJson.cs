namespace SVGLClub.Data.DTO
{
    public class SessionJson
    {
        public string TrackName { get; set; } = string.Empty;
        public string TrackConfig { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int DurationSecs { get; set; }
        public int RaceLaps { get; set; }

        public List<CarJson> Cars { get; set; } = new();
        public List<ResultJson> Result { get; set; } = new();
        public List<LapJson> Laps { get; set; } = new();
        public List<EventJson> Events { get; set; } = new();
    }
}
