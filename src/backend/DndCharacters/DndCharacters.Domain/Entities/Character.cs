using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Character : Entity
    {
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public required string Gender { get; set; }
        public decimal Height { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public required string Lore { get; set; }
        public string? DisplayImageUrl { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public Pet? Pet { get; set; }
        public ICollection<Weapon> Weapons { get; set; } = [];

        private Character() { }

        public static Character Create(
            string name,
            string? surname,
            string gender,
            decimal height,
            int age,
            DateTime birthday,
            string lore,
            string? displayImageUrl,
            CharacterRace Race,
            CharacterClass Class,
            Pet? pet)
        {
            return new Character
            {
                Name = name,
                Surname = surname,
                Gender = gender,
                Height = height,
                Age = age,
                Birthday = birthday,
                Lore = lore,
                DisplayImageUrl = displayImageUrl,
                Race = Race,
                Class = Class,
                Pet = pet
            };
        }


        public void AddWeapon(Weapon weapon)
        {
            if (Weapons.Count >= Constants.WeaponConstants.MaxWeaponsAvailable)
            {
                throw new InvalidOperationException($"Character with id {Id} already has the maximum number of weapons ({Constants.WeaponConstants.MaxWeaponsAvailable}).");
            }

            Weapons.Add(weapon);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            Weapons.Remove(weapon);
        }
    }
}
