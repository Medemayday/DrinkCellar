using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrinkCellar.Infrastructure.Repositories
{
    public class DrinkTypeRepository(DrinkCellarDbContext drinkCellarDbContext, ILogger<DrinkType> logger, DbSet<DrinkType> table) : BaseRepository<DrinkType>(drinkCellarDbContext, logger, table), IDrinkTypeRepository
    {
    }
}
