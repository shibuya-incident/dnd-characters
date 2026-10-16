using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Weapons.GetWeapons
{
    public class GetWeaponsRequest : PageListRequest<GetWeaponsSortByRequest>
    {
        public string? Name { get; set; }
        public WeaponType? Type { get; set; }
        public int? CharacterId { get; set; }
    }
}
