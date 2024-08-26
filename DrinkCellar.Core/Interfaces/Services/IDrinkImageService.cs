using DrinkCellar.Core.Services.Models;
using Microsoft.AspNetCore.Http;

namespace DrinkCellar.Core.Interfaces.Services
{
    public interface IDrinkImageService
    {
        Task<ImageResultModel> StoreImageAsync<T>(IFormFile image);
        ImageResultModel DeleteImage<T>(string fileName);
        Task<ImageResultModel> UpdateImageAsync<T>(IFormFile image, string fileName);
        ImageResultModel GetImagePath<T>(string fileName);
    }
}
