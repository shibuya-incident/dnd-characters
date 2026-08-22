using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Item : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ItemType ItemType { get; set; }
        public string? DisplayImageUrl { get; set; }
    }
}
