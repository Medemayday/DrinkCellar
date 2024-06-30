namespace DrinkCellar.Core.Entities
{
    public class Drink : BaseEntity
    {
        public int DrinkTypeId { get; set; }
        public DrinkType DrinkType { get; set; }
    }
}
