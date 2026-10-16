using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Weapons.UpdateWeapon
{
    public record UpdateWeaponResponse(
        int Id,
        string Name,
        string Description,
        decimal Weight,
        string Properties,
        WeaponType WeaponType,
        DateTime? ObtainedAt,
        string? DisplayImageUrl,
        int? CharacterId);
}
