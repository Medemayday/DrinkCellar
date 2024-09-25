using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface ICellarService : IService<Cellar>
    {
        Task<ItemResultModel<Cellar>> AddAsync(string name, int maxCapacity, bool cooled);

        Task<ItemResultModel<Cellar>> UpdateAsync(Guid id, string name, int maxCapacity, bool cooled);
    }
}
