using DrinkCellar.Core.Entities;
using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Core.Interfaces.Services;
using DrinkCellar.Core.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Core.Services
{
    public class CellarService(ICellarRepository _cellarRepository) : ICellarService
    {
        public async Task<ItemResultModel<Cellar>> AddAsync(string name, int maxCapacity, bool cooled)
        {
            try
            {
                var cellars = _cellarRepository.GetAllAsync();
                if (cellars.Any(x => x.Name == name))
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("You already have a cellar with that name. The new cellar must have a unique name")
                        }
                    };
                }

                var cellar = new Cellar
                {
                    Name = name,
                    MaxCapacity = maxCapacity,
                    Cooled = cooled,
                    Id = new Guid()
                };

                if (!await _cellarRepository.AddAsync(cellar))
                {
                    return new ItemResultModel<Cellar>
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
                return new ItemResultModel<Cellar>
                {
                    ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult(ex.Message)
                        }
                };
            }
            return new ItemResultModel<Cellar>
            {
                IsSucces = true
            };
        }

        public async Task<ItemResultModel<Cellar>> DeleteAsync(Guid id)
        {
            try
            {
                var cellar = await _cellarRepository.GetByIdAsync(id);

                if (cellar == null)
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("This cellar wasn't found in the database")
                        }
                    };
                }
                if (cellar.Drinks.Any())
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("This cellar isn't empty")
                        }
                    };
                }

                if (!await _cellarRepository.DeleteAsync(cellar.Id))
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                return new ItemResultModel<Cellar>
                {
                    IsSucces = true
                };
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Cellar>
                {
                    IsSucces = true,
                    ValidationErrors = new List<ValidationResult> 
                    { 
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Cellar>> GetAllAsync()
        {
            var cellarResult = new ItemResultModel<Cellar>();

            try
            {
                var cellars = _cellarRepository.GetAllAsync();
                if (!cellars.Any())
                {
                    cellarResult.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult ("No cellars are known")
                    };
                }
                cellarResult.Items = cellars;
                cellarResult.IsSucces = true;
                return cellarResult;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Cellar>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Cellar>> GetByIdAsync(Guid id)
        {

            try
            {
                var cellarModel = new ItemResultModel<Cellar>();
                var cellar = await _cellarRepository.GetByIdAsync(id);

                if (cellar == null)
                {
                    cellarModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This cellar was not found in the database")
                    };

                    return cellarModel;
                }

                cellarModel.IsSucces = true;
                cellarModel.Items = new List<Cellar> { cellar };
                return cellarModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Cellar>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }
        }

        public async Task<ItemResultModel<Cellar>> SearchByNameAsync(string name)
        {
            try
            {
                var cellarModel = new ItemResultModel<Cellar>();
                var cellar = await _cellarRepository.SearchByNameAsync(name);

                if (cellar == null)
                {
                    cellarModel.IsSucces = false;
                    cellarModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This cellar was not found in the database")
                    };
                    return cellarModel;
                }

                cellarModel.IsSucces = true;
                cellarModel.Items = new List<Cellar> { cellar };
                return cellarModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Cellar>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult(ex.Message)
                    }
                };
            }                    
        }

        public async Task<ItemResultModel<Cellar>> UpdateAsync(Guid id, string name, int maxCapacity, bool cooled)
        {
            try
            {
                var cellarModel = new ItemResultModel<Cellar>();
                var cellar = await _cellarRepository.GetByIdAsync(id);
                var cellars = _cellarRepository.GetAllAsync();
                if (cellar == null)
                {
                    cellarModel.IsSucces = false;
                    cellarModel.ValidationErrors = new List<ValidationResult>
                    {
                        new ValidationResult("This cellar was not found in the database")
                    };
                    return cellarModel;
                }
                
                if (cellars.Any(x => x.Name == name))
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                       new ValidationResult("You already have a cellar with that name. The new name must be unique")
                        }
                    };
                }

                cellar.Name = name;
                cellar.MaxCapacity = maxCapacity;
                cellar.Cooled = cooled;

                if (!await _cellarRepository.UpdateAsync(cellar))
                {
                    return new ItemResultModel<Cellar>
                    {
                        ValidationErrors = new List<ValidationResult>
                        {
                            new ValidationResult("Something went wrong in the system")
                        }
                    };
                }

                cellarModel.IsSucces = true;
                cellarModel.Items = new List<Cellar> { cellar };
                return cellarModel;
            }
            catch (Exception ex)
            {
                return new ItemResultModel<Cellar>
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
