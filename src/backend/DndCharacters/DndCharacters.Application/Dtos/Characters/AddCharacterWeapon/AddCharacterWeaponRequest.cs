using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.AddCharacterWeapon
{
    public record AddCharacterWeaponRequest(
        string Name,
        WeaponType WeaponType);
}
