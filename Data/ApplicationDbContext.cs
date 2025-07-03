using Microsoft.EntityFrameworkCore;

namespace SVGLClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssettoSession>()
                .HasIndex(s => s.Filename)
                .IsUnique();

            modelBuilder.Entity<TelemetryEntry>(b =>
            {
                b
                .HasIndex(e => new { e.Driver, e.Track, e.TrackVariation, e.Car, e.LapTimeMs })
                .IsUnique();

                b.Property(e => e.PositionJson).HasColumnType("nvarchar(max)");
                b.Property(e => e.GearJson).HasColumnType("nvarchar(max)");
                b.Property(e => e.SpeedJson).HasColumnType("nvarchar(max)");
                b.Property(e => e.ThrottleJson).HasColumnType("nvarchar(max)");
                b.Property(e => e.BrakeJson).HasColumnType("nvarchar(max)");
            });
        }
        
        public DbSet<AssettoSession> Sessions { get; set; }
        public DbSet<SessionCar> SessionCars { get; set; }
        public DbSet<SessionResult> SessionResult { get; set; }
        public DbSet<SessionLap> SessionLaps { get; set; }
        public DbSet<SessionEvent> SessionEvents { get; set; }
        public DbSet<TelemetryEntry> TelemetryEntries { get; set; }
    }
}
