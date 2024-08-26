using DrinkCellar.Core.Entities;

namespace DrinkCellar.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T toAdd);
        Task<bool> UpdateAsync(T toUpdate);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> SearchByNameAsync(string name);
    }
}
