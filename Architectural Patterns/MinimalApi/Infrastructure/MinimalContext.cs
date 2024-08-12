using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MinimalContext : DbContext
    {
        public MinimalContext(DbContextOptions<MinimalContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                  .UseInMemoryDatabase("Teste")
                  .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TokenHistory> TokenHistories { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
