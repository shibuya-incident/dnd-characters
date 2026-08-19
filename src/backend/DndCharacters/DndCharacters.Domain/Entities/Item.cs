using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Item : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ItemType ItemType { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; private set; }
        public bool IsOutOfStock => this.Stock == 0;
        public int ShopId { get; set; }

    }
}
