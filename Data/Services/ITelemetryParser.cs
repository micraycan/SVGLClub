namespace SVGLClub.Data.Services
{
    public interface ITelemetryParser
    {
        TelemetryData Parse(Stream stream);
        Task SaveTelemetryAsync(TelemetryData data);
        Task<List<(string Track, string Variation)>> GetTracksAsync();
        Task<List<TelemetryEntry>> GetEntriesForTrackAsync(string track, string variation);
    }
}
