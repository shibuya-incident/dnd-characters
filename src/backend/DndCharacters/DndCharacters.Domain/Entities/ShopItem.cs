namespace DndCharacters.Domain.Entities
{
    public class ShopItem : Entity
    {
        public decimal Price { get; set; }
        public int Stock { get; private set; }
        public bool IsOutOfStock => this.Stock == 0;
        public string? Description { get; set; }
        public int ShopId { get; set; }
        public int ItemId { get; set; }

    }
}
