using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Shop : Entity
    {
        public required string Name { get; set; }
        public string? DisplayImageUrl { get; set; }
        public ShopType ShopType { get; set; }
        public required string OwnerName { get; set; }
        public ICollection<ShopItem> ShopItems { get; set; } = [];

        private Shop() { }

        public static Shop Create(
            string name,
            string? displayImageUrl,
            ShopType shopType,
            string ownerName)
        {
            return new Shop
            {
                Name = name,
                DisplayImageUrl = displayImageUrl,
                ShopType = shopType,
                OwnerName = ownerName
            };
        }

        public void AddShopItem(ShopItem shopItem)
        {
            ShopItem? existingShopItem = ShopItems.FirstOrDefault(x => x.ItemId == shopItem.ItemId);

            if (existingShopItem is not null)
            {
                throw new InvalidOperationException($"The item {shopItem.ItemId} already exists in shop {shopItem.ShopId}");
            }

            ShopItems.Add(shopItem);
        }

    }
}
