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
            if (maxCapacity <= 0)
            {
                return new ItemResultModel<Cellar>
                {
                    ValidationErrors = new List<ValidationResult>
                    {
                       new ValidationResult("Minimum capacity must be greater than 0")
                    }
                };
            }

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

        public Task<ItemResultModel<Cellar>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResultModel<Cellar>> GetAllAsync()
        {
            throw new NotImplementedException();
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
