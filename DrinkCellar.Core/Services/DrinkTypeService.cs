using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Core.Interfaces.Services;
using DrinkCellar.Core.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Core.Services
{
    public class DrinkTypeService(IDrinkTypeRepository drinkTypeRepository) : IDrinkTypeService
    {
        private readonly IDrinkTypeRepository _drinkTypeRepository = drinkTypeRepository;

        public async Task<ItemResultModel<DrinkType>> AddAsync(string name)
        {
            try
            {
                var drinkTypes = _drinkTypeRepository.GetAllAsync();
                if (drinkTypes.Any(x => x.Name == name))
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("You already have a drinktype with that name. The new type must have a unique name")
                        }
                    };
                }

                var drinkType = new DrinkType
                {
                    Name = name,
                    Id = new Guid()
                };

                if (!await _drinkTypeRepository.AddAsync(drinkType))
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ItemResultModel<DrinkType>
                {
                    ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult(ex.Message)
                        }
                };
            }
            return new ItemResultModel<DrinkType>
            {
                IsSucces = true
            };
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
