using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Shop : Entity
    {
        public required string Name { get; set; }
        public string? ProfileImage { get; set; }
        public ShopType ShopType { get; set; }
        public required string OwnerName { get; set; }
        public ICollection<Item> Items { get; set; } = [];

    }
}
