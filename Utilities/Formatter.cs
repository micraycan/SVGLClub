namespace SVGLClub.Utilities
{
    public static class Formatter
    {
        public static string FormatTime(TimeSpan t) =>
            t.Hours > 0
            ? $"{t.Hours}:{t.Minutes:D2}:{t.Seconds:D2}.{t.Milliseconds:D3}"
            : t.Minutes > 0
            ? $"{t.Minutes}:{t.Seconds:D2}.{t.Milliseconds:D3}"
            : $"{t.Seconds}.{t.Milliseconds:D3}";

        public static string BuildTrackId(string trackName, string? trackConfig)
        {
            return string.IsNullOrEmpty(trackConfig)
                ? $"{trackName}|"
                : $"{trackName}|{trackConfig}";
        }

        public static string GetCarBrandSrc(string car) => $"accontent/cars/{car}/badge.png";
    }
}
