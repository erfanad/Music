using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections;

namespace MusicFall2016.Models
{
    public class PlaylistAlbums
    {
        public int AlbumID { get; set; }
        public Album Album { get; set; }
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
    }
    public class MusicDbContext : IdentityDbContext<AppUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistAlbums> PlaylistAlbums { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlaylistAlbums>()
                .HasKey(p => new { p.AlbumID, p.PlaylistID });

            modelBuilder.Entity<PlaylistAlbums>()
                .HasOne(p => p.Album)
                .WithMany(p => p.Playlists)
                .HasForeignKey(p => p.AlbumID);

            modelBuilder.Entity<PlaylistAlbums>()
                .HasOne(p => p.Playlist)
                .WithMany(p => p.Albums)
                .HasForeignKey(p => p.PlaylistID);
        }
    }
}
