using Microsoft.EntityFrameworkCore;

namespace SVGLClub.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssettoSession>()
                .HasIndex(s => s.Filename)
                .IsUnique();
        }

        public DbSet<AssettoSession> Sessions { get; set; }
        public DbSet<SessionCar> SessionCars { get; set; }
        public DbSet<SessionResult> SessionResult { get; set; }
        public DbSet<SessionLap> SessionLaps { get; set; }
        public DbSet<SessionEvent> SessionEvents { get; set; }
    }
}
