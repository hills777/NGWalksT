using Microsoft.EntityFrameworkCore;
using NGWalks.Models.Domain;

namespace NGWalks.Data
{
    public class NGWalksDbContext : DbContext
    {
        public NGWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Domain.Walk>().HasKey(w => w.id);
            modelBuilder.Entity<Models.Domain.Region>().HasKey(r => r.Id);
            modelBuilder.Entity<Models.Domain.Difficulty>().HasKey(d => d.Id);
        }
    }
    

    
}
