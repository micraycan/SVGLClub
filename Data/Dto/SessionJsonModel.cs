namespace SVGLClub.Data.Dto
{
    public class SessionJsonModel
    {
        public string TrackName { get; set; } = null!;
        public string TrackConfig { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int DurationSecs { get; set; }
        public int RaceLaps { get; set; }

        public List<CarJson> Cars { get; set; } = new();
        public List<ResultJson> Result { get; set; } = new();
        public List<LapJson> Laps { get; set; } = new();
        public List<EventJson> Events { get; set; } = new();
    }
}
