namespace SVGLClub.Data.Services
{
    public interface IContentLoader
    {
        Task GetNamesAsync(List<AssettoSession> sessions);
        string GetDisplayName(string key);
        Task<List<TrackSectionPoint>> GetTrackSectionPointsAsync(string track);
    }
}
