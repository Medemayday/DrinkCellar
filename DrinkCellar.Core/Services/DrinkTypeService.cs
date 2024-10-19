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

        public async Task<ItemResultModel<DrinkType>> DeleteAsync(Guid id)
        {
            try
            {
                var drinkType = await _drinkTypeRepository.GetByIdAsync(id);

                if (drinkType == null)
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("This drinktype wasn't found in the database")
                        }
                    };
                }
                if (drinkType.Drinks.Any())
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("This drinktype still has drinks attached")
                        }
                    };
                }

                if (!await _drinkTypeRepository.DeleteAsync(drinkType.Id))
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                return new ItemResultModel<DrinkType>
                {
                    IsSucces = true
                };
            }
            catch (Exception ex)
            {
                return new ItemResultModel<DrinkType>
                {
                    IsSucces = true,
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<DrinkType>> GetAllAsync()
        {
            var drinkTypeResult = new ItemResultModel<DrinkType>();

            try
            {
                var drinkTypes = _drinkTypeRepository.GetAllAsync();
                if (!drinkTypes.Any())
                {
                    drinkTypeResult.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult ("No drinkTypes are known")
                    };
                }
                drinkTypeResult.Items = drinkTypes;
                drinkTypeResult.IsSucces = true;
                return drinkTypeResult;
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
