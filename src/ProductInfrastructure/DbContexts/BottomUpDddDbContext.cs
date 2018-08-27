using Microsoft.EntityFrameworkCore;
using ProductInfrastructure.Models;

namespace ProductInfrastructure.DbContexts
{
    public class BottomUpDddDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseMySQL(Config.ConnectionString);

        }
    }
}
