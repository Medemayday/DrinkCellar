using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface IDrinkTypeService: IService<DrinkType>
    {
        Task<ItemResultModel<DrinkType>> AddAsync(string name);

        Task<ItemResultModel<DrinkType>> UpdateAsync(Guid id, string name, List<Drink> drinks);
    }
}
