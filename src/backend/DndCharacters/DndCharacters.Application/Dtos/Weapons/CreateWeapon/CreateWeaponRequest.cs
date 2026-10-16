using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Weapons.CreateWeapon
{
    public record CreateWeaponRequest(
        string Name,
        string Description,
        decimal Weight,
        string Properties,
        WeaponType WeaponType,
        string? DisplayImageUrl,
        int? CharacterId);
}
