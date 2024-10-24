using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Core.Interfaces.Services;
using DrinkCellar.Core.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Core.Services
{
    public class DrinkService(IDrinkRepository drinkRepository, IDrinkTypeService drinkTypeService, ICellarService cellarService) : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository = drinkRepository;
        private readonly IDrinkTypeService _drinkTypeService = drinkTypeService;
        private readonly ICellarService _cellarService = cellarService;

        public async Task<ItemResultModel<Drink>> AddAsync(string name, Guid drinkTypeId, Guid cellarId, int amount, DateTime? expirationDate)
        {
            try
            {
                var cellar = await _cellarService.GetByIdAsync(cellarId);
                var drinkType = await _drinkTypeService.GetByIdAsync(drinkTypeId);
                var drinks = _drinkRepository.GetAllAsync();

                if (!cellar.IsSucces || !drinkType.IsSucces)
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            cellar.ValidationErrors.FirstOrDefault(),
                            drinkType.ValidationErrors.FirstOrDefault()
                        }
                    };
                }

                if (!cellar.Items.Any() || !drinkType.Items.Any())
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("No cellar or drinktype was found")
                        }
                    };
                }

                var drink = new Drink
                {
                    Name = name,
                    DrinkTypeId = drinkTypeId,
                    CellarId = cellarId,
                    Amount = amount,
                    ExperiationDate = expirationDate
                };

                var alreadyExistingDrink = drinks.FirstOrDefault(x => x.Name == name && x.ExperiationDate == expirationDate && x.CellarId == cellarId);

                if (alreadyExistingDrink != null)
                {
                    alreadyExistingDrink.Amount += amount;

                    if (!await _drinkRepository.UpdateAsync(alreadyExistingDrink))
                    {
                        return new ItemResultModel<Drink>
                        {
                            ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                        };
                    }
                }

                if (!await _drinkRepository.AddAsync(drink))
                {
                    return new ItemResultModel<Drink>
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
                return new ItemResultModel<Drink>
                {
                    ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult(ex.Message)
                        }
                };
            }
            return new ItemResultModel<Drink>
            {
                IsSucces = true
            };
        }

        public async Task<ItemResultModel<Drink>> DeleteAsync(Guid id)
        {
            try
            {
                var drink = await _drinkRepository.GetByIdAsync(id);

                if (drink == null)
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("This drink wasn't found in the database")
                        }
                    };
                }

                if (!await _drinkRepository.DeleteAsync(drink.Id))
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                return new ItemResultModel<Drink>
                {
                    IsSucces = true
                };
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Drink>
                {
                    IsSucces = true,
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Drink>> GetAllAsync()
        {
            var drinkResult = new ItemResultModel<Drink>();

            try
            {
                var drinks = _drinkRepository.GetAllAsync();
                if (!drinks.Any())
                {
                    drinkResult.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult ("No drinks are known")
                    };
                }
                drinkResult.Items = drinks;
                drinkResult.IsSucces = true;
                return drinkResult;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Drink>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Drink>> GetByIdAsync(Guid id)
        {
            try
            {
                var drinkModel = new ItemResultModel<Drink>();
                var drink = await _drinkRepository.GetByIdAsync(id);

                if (drink == null)
                {
                    drinkModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drink was not found in the database")
                    };

                    return drinkModel;
                }

                drinkModel.IsSucces = true;
                drinkModel.Items = new List<Drink> { drink };
                return drinkModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Drink>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Drink>> SearchByNameAsync(string name)
        {
            try
            {
                var drinkModel = new ItemResultModel<Drink>();
                var drinks = await _drinkRepository.SearchByNameAsync(name);

                if (drinks.Any())
                {
                    drinkModel.IsSucces = false;
                    drinkModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drink was not found in the database")
                    };
                    return drinkModel;
                }

                drinkModel.IsSucces = true;
                drinkModel.Items = drinks;
                return drinkModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Drink>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Drink>> UpdateAsync(Guid id, string name, Guid drinkTypeId, Guid cellarId, int amount, DateTime? expirationDate)
        {
            try
            {
                var drinkModel = new ItemResultModel<Drink>();
                var drink = await _drinkRepository.GetByIdAsync(id);
                var drinks = _drinkRepository.GetAllAsync();
                if (drink == null)
                {
                    drinkModel.IsSucces = false;
                    drinkModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This drink was not found in the database")
                    };
                    return drinkModel;
                }

                if (drinks.Any(x => x.Name == name && x.CellarId == cellarId))
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                       new ValidationResult("You already have a drink with that name in this cellar. The new name must be unique")
                        }
                    };
                }

                drink.Name = name;
                drink.DrinkTypeId = drinkTypeId;
                drink.CellarId = cellarId;
                drink.Amount = amount;
                drink.ExperiationDate = expirationDate;

                if (!await _drinkRepository.UpdateAsync(drink))
                {
                    return new ItemResultModel<Drink>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                drinkModel.IsSucces = true;
                drinkModel.Items = new List<Drink> { drink };
                return drinkModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Drink>
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

