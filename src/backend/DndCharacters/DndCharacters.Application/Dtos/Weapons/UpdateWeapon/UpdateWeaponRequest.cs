using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Weapons.UpdateWeapon
{
    public record UpdateWeaponRequest(
        string Name,
        string Description,
        decimal Weight,
        string Properties,
        WeaponType WeaponType,
        string? DisplayImageUrl,
        int? CharacterId);
}
