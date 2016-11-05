using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicFall2016.Models
{
    public class MusicDbContext : IdentityDbContext<AppUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
