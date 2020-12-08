using MatrizConhecimento.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrizConhecimento.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOption) : base(dbContextOption)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics {get;set;}
        public DbSet<Matter>Matters {get;set;}
        public DbSet<Rating> Ratings {get;set;}
        public DbSet<RatingHistory> RatingHistories {get;set;}

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Rating>().HasKey(r => new { r.TopicId, r.MatterId, r.UserId });
        }
}
}
