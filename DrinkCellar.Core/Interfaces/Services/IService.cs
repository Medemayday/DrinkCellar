using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<ItemResultModel<T>> GetAllAsync();

        Task<ItemResultModel<T>> GetByIdAsync(Guid id);


        Task<ItemResultModel<T>> DeleteAsync(Guid id);

        Task<ItemResultModel<T>> SearchByNameAsync(string name);
    }
}
