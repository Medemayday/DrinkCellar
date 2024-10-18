using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Api.DTOs
{
    public class BaseRequestDTO
    {
        [Required(ErrorMessage = "Please provide a name")]
        [MaxLength(30)]
        public string Name { get; set; }
            
    }
}
