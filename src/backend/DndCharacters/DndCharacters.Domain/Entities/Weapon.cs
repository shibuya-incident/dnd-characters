using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Weapon : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public WeaponType WeaponType { get; set; }
        public int? CharacterId { get; set; }
    }
}
