namespace DrinkCellar.Core.Entities
{
    public class Drink: BaseEntity
    {
        public Guid DrinkTypeId { get; set; }
        public DrinkType DrinkType { get; set; }
        public Guid CellarId { get; set; }
        public Cellar Cellar { get; set; } 
        public string Note { get; set; }
    }
}
