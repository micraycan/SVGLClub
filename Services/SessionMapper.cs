using SVGLClub.Data;
using SVGLClub.Data.DTO;

namespace SVGLClub.Services
{
    public class SessionMapper : ISessionMapper
    {
        public AssettoSession Map(SessionJson dto, string filename)
        {
            if (dto == null) { throw new ArgumentNullException(nameof(dto)); }

            AssettoSession session = new AssettoSession
            {
                Filename = filename,
                TrackName = dto.TrackName,
                TrackConfig = dto.TrackConfig,
                SessionType = dto.Type,
                DurationSecs = dto.DurationSecs,
                RaceLaps = dto.RaceLaps,
                Cars = MapCars(dto),
                Results = MapResults(dto),
                Laps = MapLaps(dto),
                Events = MapEvents(dto)
            };

            return session;
        }

        private List<SessionCar> MapCars(SessionJson dto)
        {
            return dto.Cars?.Select(c => new SessionCar
            {
                CarId = c.CarId,
                DriverName = c.Driver.Name,
                DriverGuid = c.Driver.Guid,
                Model = c.Model,
                Skin = c.Skin,
                BallastKG = c.BallastKG,
                Restrictor = c.Restrictor
            }).ToList() ?? new();
        }

        private List<SessionResult> MapResults(SessionJson dto)
        {
            return dto.Result?.Select(r => new SessionResult
            {
                DriverName = r.DriverName,
                DriverGuid = r.DriverGuid,
                CarId = r.CarId,
                CarModel = r.CarModel,
                BestLap = r.BestLap,
                TotalTime = r.TotalTime,
                BallastKG = r.BallastKG,
                Restrictor = r.Restrictor
            }).ToList() ?? new();
        }

        private List<SessionLap> MapLaps(SessionJson dto)
        {
            return dto.Laps?.Select(l =>
            {
                int[] sectors = (l.Sectors ?? Array.Empty<long>())
                    .Select(s => s <= int.MaxValue ? (int)s : 0)
                    .ToArray();

                return new SessionLap
                {
                    DriverName = l.DriverName,
                    DriverGuid = l.DriverGuid,
                    CarId = l.CarId,
                    CarModel = l.CarModel,
                    Timestamp = l.Timestamp,
                    LapTime = l.LapTime,
                    Sector1 = sectors.Length > 0 ? sectors[0] : 0,
                    Sector2 = sectors.Length > 1 ? sectors[1] : 0,
                    Sector3 = sectors.Length > 2 ? sectors[2] : 0,
                    Cuts = l.Cuts,
                    Tyre = l.Tyre,
                    BallastKG = l.BallastKG,
                    Restrictor = l.Restrictor
                };
            }).ToList() ?? new();
        }

        private List<SessionEvent> MapEvents(SessionJson dto)
        {
            return dto.Events?.Select(e => new SessionEvent
            {
                Type = e.Type,
                CarId = e.CarId,
                DriverName = e.Driver.Name,
                DriverGuid = e.Driver.Guid,
                OtherCarId = e.OtherCarId,
                OtherDrivername = e.OtherDriver.Name,
                ImpactSpeed = e.ImpactSpeed
            }).ToList() ?? new();
        }
    }

    public interface ISessionMapper
    {
        AssettoSession Map(SessionJson dto, string filename);
    }
}
