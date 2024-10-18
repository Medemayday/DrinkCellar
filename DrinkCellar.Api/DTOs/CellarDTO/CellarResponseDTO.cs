namespace DrinkCellar.Api.DTOs.CellarDTO
{
    public class CellarResponseDTO : BaseResponseDTO
    {
        public int MaxCapacity { get; set; }
        public bool Cooled { get; set; }
    }
}
