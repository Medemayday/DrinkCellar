using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrinkCellar.Infrastructure.Repositories
{
    public class CellarRepository(DrinkCellarDbContext drinkCellarDbContext, ILogger<Cellar> logger, DbSet<Cellar> table) : BaseRepository<Cellar>(drinkCellarDbContext, logger, table), ICellarRepository
    {
    }
}
