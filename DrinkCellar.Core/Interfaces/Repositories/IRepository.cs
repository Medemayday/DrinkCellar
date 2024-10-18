using DrinkCellar.Core.Entities;

namespace DrinkCellar.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T toAdd);
        Task<bool> UpdateAsync(T toUpdate);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SearchByNameAsync(string name);
    }
}
