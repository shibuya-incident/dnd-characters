using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.GetCharacters
{
    public record GetCharactersListItemResponse(
        int Id,
        string Name,
        CharacterRace CharacterRace,
        CharacterClass CharacterClass,
        string? DisplayImageUrl);
}
