namespace SVGLClub.Data
{
    public static class Util
    {
        public static string FormatTime(TimeSpan t) =>
            t.Hours > 0
            ? $"{t.Hours}:{t.Minutes:D2}:{t.Seconds:D2}.{t.Milliseconds:D3}"
            : t.Minutes > 0
            ? $"{t.Minutes}:{t.Seconds:D2}.{t.Milliseconds:D3}"
            : $"{t.Seconds}.{t.Milliseconds:D3}";
    }
}
