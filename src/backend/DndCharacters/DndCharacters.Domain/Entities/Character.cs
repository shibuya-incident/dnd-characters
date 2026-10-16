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
        public CharacterRace CharacterRace { get; set; }
        public CharacterClass CharacterClass { get; set; }
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
            CharacterRace characterRace,
            CharacterClass characterClass,
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
                CharacterRace = characterRace,
                CharacterClass = characterClass,
                Pet = pet
            };
        }
    }
}
