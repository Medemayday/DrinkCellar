using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrinkCellar.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>(DrinkCellarDbContext drinkCellarDbContext, ILogger<T> logger, DbSet<T> table) : IRepository<T> where T : BaseEntity
    {
        protected readonly DrinkCellarDbContext _drinkCellarDbContext = drinkCellarDbContext;
        protected ILogger<T> _logger = logger;
        protected DbSet<T> _table = table;

        public async Task<bool> AddAsync(T toAdd)
        {
            await _table.AddAsync(toAdd);
            return await Save();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var t = await GetByIdAsync(id);
            _table.Remove(t);
            return await Save();
        }
        public async Task<bool> UpdateAsync(T toUpdate)
        {
            _table.Update(toUpdate);
            return await Save();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _table
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public virtual async Task<T> SearchByNameAsync(string name)
        {
            return await _table
                .FirstOrDefaultAsync(b => b.Name == name);
        }
                
        private async Task<bool> Save()
        {
            try
            {
                await _drinkCellarDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException.Message);
                return false;
            }
        }
    }
}
