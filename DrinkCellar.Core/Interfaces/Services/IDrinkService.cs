using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface IDrinkService
    {
        Task<ItemResultModel<Cellar>> AddAsync(string name, Guid drinkTypeId, Guid cellarId);

        Task<ItemResultModel<Cellar>> UpdateAsync(Guid id, string name, Guid drinkTypeId, Guid cellarId);
    }
}
