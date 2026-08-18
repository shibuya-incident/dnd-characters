using DndCharacters.Domain.Enum;

namespace DndCharacters.Domain.Entities
{
    public class Character : Entity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int Age { get; set; }
        public required string Personality { get; set; }
        public required string ProfileImage { get; set; }

        public CharacterRace CharacterRace { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public ICollection<Weapon> Weapons { get; set; } = [];
    }
}
