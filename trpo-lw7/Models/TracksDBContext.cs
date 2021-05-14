using Microsoft.EntityFrameworkCore;

namespace trpo_lw7.Models
{
    public class TracksDBContext : DbContext
    {
        public TracksDBContext(DbContextOptions<TracksDBContext> options) : base(options)
        {
        }

        // Коллекции сущностей
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>().ToTable("Musician");
            modelBuilder.Entity<Track>().ToTable("Track");
        }
    }
}