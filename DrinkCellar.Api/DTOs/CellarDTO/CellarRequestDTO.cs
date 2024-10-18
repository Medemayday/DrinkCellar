using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Api.DTOs.CellarDTO
{
    public class CellarRequestDTO : BaseRequestDTO
    {
        public bool Cooled { get; set; }

        [Required(ErrorMessage = "Please provide a maximum capacity")]
        [Range(0, 100, ErrorMessage= "Please provide a capacity between 1 and 100")]
        public int MaxCapacity { get; set; }
    }
}
