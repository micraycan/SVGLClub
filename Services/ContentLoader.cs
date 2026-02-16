using SVGLClub.Data;
using System.Text.Json;

namespace SVGLClub.Services
{
    public class ContentLoader : IContentLoader
    {
        private readonly string _contentFolder;
        Dictionary<string, string> _contentNames = new();

        public ContentLoader(IWebHostEnvironment env)
        {
            _contentFolder = Path.Combine(env.WebRootPath!, "accontent");
        }

        public string GetDisplayName(string key)
        {
            return _contentNames[key];
        }

        public async Task LoadAllContentNamesAsync()
        {
            await LoadAllCarNamesAsync();
            await LoadAllTrackNamesAsync();
        }

        private async Task LoadAllCarNamesAsync()
        {
            string carsRoot = Path.Combine(_contentFolder, "cars");

            if (!Directory.Exists(carsRoot)) { return; }

            foreach (string carDir in Directory.EnumerateDirectories(carsRoot))
            {
                string carFolderName = Path.GetFileName(carDir);

                if (_contentNames.ContainsKey(carFolderName)) { continue; }

                _contentNames[carFolderName] = await GetCarNameAsync(carFolderName);
            }
        }

        private async Task LoadAllTrackNamesAsync()
        {
            string tracksRoot = Path.Combine(_contentFolder, "tracks");

            if (!Directory.Exists(tracksRoot)) { return; }

            foreach (string trackDir in Directory.EnumerateDirectories(tracksRoot))
            {
                string trackFolderName = Path.GetFileName(trackDir);

                foreach (string layoutDir in Directory.EnumerateDirectories(trackDir))
                {
                    string layoutName = Path.GetFileName(layoutDir);
                    if (layoutName == "default") { layoutName = string.Empty; }
                    string fullTrackKey = $"{trackFolderName}|{layoutName}";

                    if (_contentNames.ContainsKey(fullTrackKey)) { continue; }

                    _contentNames[fullTrackKey] = await GetTrackNameAsync(trackFolderName, layoutName);
                }
            }
        }

        private async Task<string> GetCarNameAsync(string car)
        {
            if (string.IsNullOrEmpty(car)) { return "error"; }
            string path = Path.Combine(_contentFolder, "cars", car, "ui_car.json");

            if (!File.Exists(path)) { return car + "___"; }

            string data = await File.ReadAllTextAsync(path);
            string name = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("name").ToString() ?? car + "___";

            return name;
        }

        private async Task<string> GetTrackNameAsync(string track, string config)
        {
            if (string.IsNullOrEmpty(track)) { return "error"; }

            string trackLayout = config != string.Empty ? config : "default";
            string path = Path.Combine(_contentFolder, "tracks", track, trackLayout, "ui_track.json");

            if (!File.Exists(path)) { return track + "___"; }

            string data = await File.ReadAllTextAsync(path);
            string name = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("name").ToString() ?? track + "___";

            return name;
        }
    }

    public interface IContentLoader
    {
        Task LoadAllContentNamesAsync();
        string GetDisplayName(string key);
    }
}
