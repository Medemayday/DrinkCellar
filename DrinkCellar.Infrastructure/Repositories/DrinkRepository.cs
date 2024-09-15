using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrinkCellar.Infrastructure.Repositories
{
    public class DrinkRepository(DrinkCellarDbContext drinkCellarDbContext, ILogger<Drink> logger, DbSet<Drink> table) : BaseRepository<Drink>(drinkCellarDbContext, logger, table), IDrinkRepository
    {
    }
}
