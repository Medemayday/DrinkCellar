namespace DrinkCellar.Core.Entities
{
    public class Drink: BaseEntity
    {
        public Guid DrinkTypeId { get; set; }
        public DrinkType DrinkType { get; set; }
        public Guid CellarId { get; set; }
        public Cellar cellar { get; set; } 
    }
}
