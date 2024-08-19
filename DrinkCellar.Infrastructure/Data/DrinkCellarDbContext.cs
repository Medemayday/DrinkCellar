using DrinkCellar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrinkCellar.Infrastructure.Data
{
    public class DrinkCellarDbContext : DbContext
    {
        public DbSet<Cellar> Cellars { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkImage> DrinkImages { get; set; }
        public DbSet<DrinkType> DrinkTypes { get; set; }

        public DrinkCellarDbContext(DbContextOptions<DrinkCellarDbContext> options) : base(options)
        {
            
        }
    }
}
