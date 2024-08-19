namespace DrinkCellar.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
