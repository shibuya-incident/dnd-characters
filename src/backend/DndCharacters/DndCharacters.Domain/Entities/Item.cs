using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Item : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ItemType ItemType { get; set; }
        public string? DisplayImageUrl { get; set; }

        public static Item Create(
            string name,
            string description,
            ItemType itemType,
            string? displayImageUrl)
        {
            return new Item
            {
                Name = name,
                Description = description,
                ItemType = itemType,
                DisplayImageUrl = displayImageUrl
            };
        }

    }
}
