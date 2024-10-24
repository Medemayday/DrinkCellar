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

        public async Task<ItemResultModel<DrinkType>> GetByIdAsync(Guid id)
        {
            try
            {
                var drinkTypeModel = new ItemResultModel<DrinkType>();
                var drinkType = await _drinkTypeRepository.GetByIdAsync(id);

                if (drinkType == null)
                {
                    drinkTypeModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drinktype was not found in the database")
                    };

                    return drinkTypeModel;
                }

                drinkTypeModel.IsSucces = true;
                drinkTypeModel.Items = new List<DrinkType> { drinkType };
                return drinkTypeModel;
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

        public async Task<ItemResultModel<DrinkType>> SearchByNameAsync(string name)
        {
            try
            {
                var drinkTypeModel = new ItemResultModel<DrinkType>();
                var drinkTypes = await _drinkTypeRepository.SearchByNameAsync(name);

                if (!drinkTypes.Any())
                {
                    drinkTypeModel.IsSucces = false;
                    drinkTypeModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drinktype was not found in the database")
                    };
                    return drinkTypeModel;
                }

                drinkTypeModel.IsSucces = true;
                drinkTypeModel.Items = drinkTypes;
                return drinkTypeModel;
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

        public async Task<ItemResultModel<DrinkType>> UpdateAsync(Guid id, string name)
        {
            try
            {
                var drinkTypeModel = new ItemResultModel<DrinkType>();
                var drinkType = await _drinkTypeRepository.GetByIdAsync(id);
                var drinkTypes = _drinkTypeRepository.GetAllAsync();
                if (drinkType == null)
                {
                    drinkTypeModel.IsSucces = false;
                    drinkTypeModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drinktype was not found in the database")
                    };
                    return drinkTypeModel;
                }

                if (drinkTypes.Any(x => x.Name == name))
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                       new ValidationResult("You already have a drinktype with that name. The new name must be unique")
                        }
                    };
                }

                drinkType.Name = name;

                if (!await _drinkTypeRepository.UpdateAsync(drinkType))
                {
                    return new ItemResultModel<DrinkType>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                drinkTypeModel.IsSucces = true;
                drinkTypeModel.Items = new List<DrinkType> { drinkType };
                return drinkTypeModel;
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
    }
}
