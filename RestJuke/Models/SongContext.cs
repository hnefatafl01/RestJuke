using System;
using Microsoft.EntityFrameworkCore;

namespace RestJuke.Models
{
    public class SongContext : DbContext
    {
        public SongContext(DbContextOptions<SongContext> options) : base(options)
        { 
        }
		public DbSet<Song> SongList { get; set; }
    }
}
