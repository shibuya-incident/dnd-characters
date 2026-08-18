using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Weapon : Entity
    {
        public int ItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public WeaponType WeaponType { get; set; }

    }
}
