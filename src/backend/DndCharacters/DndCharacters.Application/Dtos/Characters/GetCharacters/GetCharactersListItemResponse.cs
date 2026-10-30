using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.GetCharacters
{
    public record GetCharactersListItemResponse(
        int Id,
        string Name,
        CharacterRace Race,
        CharacterClass Class,
        string? DisplayImageUrl,
        DateTime UpdatedAt);
}
