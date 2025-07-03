using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace SVGLClub.Data.Services
{
    public class TelemetryParser : ITelemetryParser
    {
        private readonly ApplicationDbContext _db;

        public TelemetryParser(ApplicationDbContext db) => _db = db;

        public TelemetryData Parse(Stream stream)
        {
            using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);

            reader.ReadInt32();

            string ReadVar()
            {
                var size = reader.ReadInt32();
                if (size == 0) { return string.Empty; }
                var bytes = reader.ReadBytes(size);
                return Encoding.UTF8.GetString(bytes);
            }

            var data = new TelemetryData()
            {
                User = ReadVar(),
                Track = ReadVar(),
                Car = ReadVar(),
                TrackVariation = ReadVar(),
                LapTimeMs = reader.ReadInt32(),
                NumDataPoints = reader.ReadInt32()
            };

            for (int i = 0; i < data.NumDataPoints; i++)
            {
                int gear = reader.ReadInt32();
                float pos = reader.ReadSingle();
                float speed = reader.ReadSingle();
                float throttle = reader.ReadSingle();
                float brake = reader.ReadSingle();

                data.Gear.Add(gear - 1);
                data.Position.Add(pos);
                data.Speed.Add(speed);
                data.Throttle.Add(throttle);
                data.Brake.Add(brake);
            }

            return data;
        }

        public async Task SaveTelemetryAsync(TelemetryData data)
        {
            var exists = await _db.TelemetryEntries.AnyAsync(e =>
                e.Driver == data.User &&
                e.Track == data.Track &&
                e.TrackVariation == data.TrackVariation &&
                e.Car == data.Car &&
                e.LapTimeMs == data.LapTimeMs);

            if (exists) { return; }

            var entry = new TelemetryEntry
            {
                Driver = data.User,
                Track = data.Track,
                TrackVariation = data.TrackVariation,
                Car = data.Car,
                LapTimeMs = data.LapTimeMs,
                NumDataPoints = data.NumDataPoints,
                PositionJson = JsonSerializer.Serialize(data.Position),
                GearJson = JsonSerializer.Serialize(data.Gear),
                SpeedJson = JsonSerializer.Serialize(data.Speed),
                ThrottleJson = JsonSerializer.Serialize(data.Throttle),
                BrakeJson = JsonSerializer.Serialize(data.Brake),
            };

            _db.TelemetryEntries.Add(entry);
            await _db.SaveChangesAsync();
        }

        public async Task<List<(string Track, string Variation)>> GetTracksAsync()
        {
            var anon = await _db.TelemetryEntries
                    .Select(e => new { e.Track, e.TrackVariation })
                    .Distinct()
                    .ToListAsync();

            return anon
                .Select(x => (x.Track, x.TrackVariation))
                .ToList();
        }

        public async Task<List<TelemetryEntry>> GetEntriesForTrackAsync(string track, string variation)
        {
            return await _db.TelemetryEntries
                    .Where(e => e.Track == track && e.TrackVariation == variation)
                    .ToListAsync();
        }
    }
}
