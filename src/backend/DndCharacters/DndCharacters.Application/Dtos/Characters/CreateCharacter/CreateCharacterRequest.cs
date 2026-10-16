using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.CreateCharacter
{
    public record CreateCharacterRequest(
        string Name,
        string? Surname,
        string Gender,
        decimal Height,
        int Age,
        DateTime Birthday,
        string Lore,
        string? DisplayImageUrl,
        CharacterRace Race,
        CharacterClass Class,
        Pet? Pet);
}
