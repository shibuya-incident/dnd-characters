using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Weapons.GetWeapons
{
    public record GetWeaponsListItemResponse(
        int Id,
        string Name,
        WeaponType WeaponType,
        DateTime? ObtainedAt,
        DateTime CreatedAt,
        string? DisplayImageUrl,
        int? CharacterId);
}
