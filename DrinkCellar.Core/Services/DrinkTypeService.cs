using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Core.Interfaces.Services;
using DrinkCellar.Core.Services.Models;

namespace DrinkCellar.Core.Services
{
    public class DrinkTypeService(IDrinkTypeRepository drinkTypeRepository) : IDrinkTypeService
    {
        private readonly IDrinkTypeRepository _drinkTypeRepository = drinkTypeRepository;

        public Task<ItemResultModel<DrinkType>> AddAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<DrinkType>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<DrinkType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<DrinkType>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<DrinkType>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<DrinkType>> UpdateAsync(Guid id, string name, List<Drink> drinks)
        {
            throw new NotImplementedException();
        }
    }
}
