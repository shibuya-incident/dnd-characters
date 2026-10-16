using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.GetCharacterWeapons
{
    public record GetCharacterWeaponsListItemResponse(
        int Id,
        int CharacterId,
        string Name,
        string? DisplayImageUrl,
        WeaponType WeaponType);
}
