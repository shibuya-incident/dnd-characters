using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Weapon : Entity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Weight { get; set; }
        public required string Properties { get; set; }
        public WeaponType WeaponType { get; set; }
        public DateTime? ObtainedAt { get; set; }
        public string? DisplayImageUrl { get; set; }
        public int? CharacterId { get; set; }

        private Weapon() { }

        public static Weapon Create(
            string name,
            string description,
            decimal weight,
            string properties,
            WeaponType weaponType,
            string? displayImageUrl,
            int? characterId)
        {
            return new Weapon
            {
                Name = name,
                Description = description,
                Weight = weight,
                Properties = properties,
                WeaponType = weaponType,
                DisplayImageUrl = displayImageUrl,
                CharacterId = characterId
            };
        }
    }
}
