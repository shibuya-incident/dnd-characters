using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Shop : Entity
    {
        public required string Name { get; set; }
        public string? DisplayImage { get; set; }
        public ShopType ShopType { get; set; }
        public required string OwnerName { get; set; }
        public ICollection<ShopItem> ShopItems { get; set; } = [];

        public static Shop Create(
            string name,
            string? profileImage,
            ShopType shopType,
            string ownerName)
        {
            return new Shop
            {
                Name = name,
                DisplayImage = profileImage,
                ShopType = shopType,
                OwnerName = ownerName
            };
        }
    }
}
