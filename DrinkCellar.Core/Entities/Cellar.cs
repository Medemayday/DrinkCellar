namespace DrinkCellar.Core.Entities
{
    public class Cellar : BaseEntity
    {
        public int MaxCapacity { get; set; }
        public bool Cooled { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}
