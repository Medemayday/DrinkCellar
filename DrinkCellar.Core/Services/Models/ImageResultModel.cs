using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Core.Services.Models
{
    public class ImageResultModel
    {
        public bool IsSuccess { get; set; }

        public ValidationResult Error { get; set; }

        public string ImageUrl { get; set; }
    }
}
