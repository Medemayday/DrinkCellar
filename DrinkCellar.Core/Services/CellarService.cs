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
                var cellars = await _cellarRepository.GetAllAsync();
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemResultModel<Cellar>> GetAllAsync()
        {
            var cellarResult = new ItemResultModel<Cellar>();

            try
            {
                var cellars = await _cellarRepository.GetAllAsync();
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

        public Task<ItemResultModel<Cellar>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<Cellar>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<Cellar>> UpdateAsync(Guid id, string name, int maxCapacity, bool cooled, List<Drink> drinks)
        {
            throw new NotImplementedException();
        }
    }
}
