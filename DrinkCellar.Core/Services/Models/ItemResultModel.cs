using System.ComponentModel.DataAnnotations;

namespace DrinkCellar.Core.Services.Models
{
    public class ItemResultModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public IEnumerable<ValidationResult> ValidationErrors { get; set; }
        public bool IsSucces { get; set; }
    }
}
