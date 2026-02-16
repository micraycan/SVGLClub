namespace SVGLClub.Data
{
    public class AssettoSession
    {
        public int Id { get; set; }
        public string Filename { get; set; } = string.Empty;
        public string TrackName { get; set; } = string.Empty;
        public string TrackConfig { get; set; } = string.Empty;
        public string SessionType { get; set; } = string.Empty;
        public int DurationSecs { get; set; }
        public int RaceLaps { get; set; }

        public ICollection<SessionCar>? Cars { get; set; }
        public ICollection<SessionResult>? Results { get; set; }
        public ICollection<SessionLap>? Laps { get; set; }
        public ICollection<SessionEvent>? Events { get; set; }

        public DateTime SessionDate
        {
            get
            {
                var name = Path.GetFileNameWithoutExtension(Filename);
                var parts = name.Split('_');
                if (parts.Length < 5) { return DateTime.MinValue; }

                if (int.TryParse(parts[0], out var year)
                    && int.TryParse(parts[1], out var month)
                    && int.TryParse(parts[2], out var day)
                    && int.TryParse(parts[3], out var hour)
                    && int.TryParse(parts[4], out var minute))
                {
                    return new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc).ToLocalTime();
                }

                return DateTime.MinValue;
            }
        }
    }
}
