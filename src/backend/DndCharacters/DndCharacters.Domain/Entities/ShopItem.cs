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

        private ShopItem() { }

        public static ShopItem Create(
            decimal price,
            int stock,
            string? description,
            int shopId,
            int itemId)
        {
            return new ShopItem
            {
                Price = price,
                Stock = stock,
                Description = description,
                ShopId = shopId,
                ItemId = itemId
            };
        }

        public void AddStock()
        {
            this.Stock += 1;
        }
    }
}
