using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.EntityFramework.Contexts
{
    public class SpotifyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=RECEP;Database=SpotifyDbRecep;User ID=sa;Password=;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistFollowing> PlaylistFollowings { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<UserAlbum> UserAlbums { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibraryPlaylist> LibraryPlaylists { get; set; }
        public DbSet<LibraryAlbum> LibraryAlbums { get; set; }
    }
}
