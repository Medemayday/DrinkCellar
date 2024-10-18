using DrinkCellar.Api.DTOs.CellarDTO;
using DrinkCellar.Core.Entities;

namespace DrinkCellar.Api.Extensions
{
    public static class MappingExtensions
    {
        public static CellarResponseDTO ToResponseDTO (this Cellar cellar)
        {
            if (cellar == null) return null;

            var dTO = new CellarResponseDTO
            {
                Id = cellar.Id,
                Name = cellar.Name,
                MaxCapacity = cellar.MaxCapacity,
                Cooled = cellar.Cooled,
            };
            return dTO;
        }

        public static IEnumerable<CellarResponseDTO> ToResponseDTOS (this IEnumerable<Cellar> cellars)
        {
            return cellars?.Select(c => c.ToResponseDTO());
        }
    }
}
