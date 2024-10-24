using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface IDrinkService: IService<Drink>
    {
        Task<ItemResultModel<Drink>> AddAsync(string name, Guid drinkTypeId, Guid cellarId, int amount, DateTime? expirationDate);

        Task<ItemResultModel<Drink>> UpdateAsync(Guid id, string name, Guid drinkTypeId, Guid cellarId, int amount, DateTime? expirationDate);
    }
}
