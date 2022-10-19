using Google.Cloud.Kms.V1;
using Microsoft.EntityFrameworkCore;
using MusicManagementSystem.Models;

namespace MusicManagementSystem.Data
{
    public class MusicManagemetSystemDbContext : DbContext
    {
        public MusicManagemetSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Songs> Songs { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<SongsArtists> SongsArtists { get; set; }
        public DbSet<SongsGenres> SongsGenres { get; set; }
        public DbSet<SongsImagesPaths> SongsImagesPaths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Songs>()
                .HasMany(s => s.SongsArtistsPairs);
            modelBuilder.Entity<Songs>()
                .HasMany(s => s.SongsGenresPairs);

            modelBuilder.Entity<Artists>()
                .HasMany(a => a.SongsAuthorsPairs);

            modelBuilder.Entity<SongsArtists>()
                .HasKey(sa => new { sa.SongId, sa.ArtistId });

            modelBuilder.Entity<SongsGenres>()
                .HasKey(sg => new { sg.SongId, sg.GenreId });

            modelBuilder.Entity<SongsImagesPaths>()
                .HasKey(si => new { si.SongId, si.ImagePathId });

        }
    }
}
