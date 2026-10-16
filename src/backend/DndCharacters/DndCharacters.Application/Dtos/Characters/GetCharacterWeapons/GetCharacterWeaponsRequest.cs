using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.GetCharacterWeapons
{
    public class GetCharacterWeaponsRequest : PageListRequest<GetCharacterWeaponsSortByRequest>
    {
        public string? Name { get; set; }
        public WeaponType? WeaponType { get; set; }
    }
}
