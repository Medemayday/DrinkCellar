namespace DrinkCellar.Core.Entities
{
    public class DrinkImage : BaseEntity
    {
        public string Path { get; set; }
        public Guid DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}
