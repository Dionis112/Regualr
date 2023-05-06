using Microsoft.EntityFrameworkCore;
using Regular.Console.Domain;

namespace Regular.Console.Infrastructure.Repositories
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Configuration> Configurations { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Regular.db");
        }
    }
}
