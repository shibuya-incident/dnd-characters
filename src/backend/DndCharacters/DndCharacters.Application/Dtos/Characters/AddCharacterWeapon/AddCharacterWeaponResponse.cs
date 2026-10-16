using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.AddCharacterWeapon
{
    public record AddCharacterWeaponResponse(
        int Id,
        int? CharacterId,
        string Name,
        WeaponType WeaponType,
        DateTime? ObtainedAt,
        string? DisplayImageUrl);
}
