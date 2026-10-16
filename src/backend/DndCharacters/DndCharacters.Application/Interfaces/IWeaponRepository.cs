using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Weapons.GetWeapons;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IWeaponRepository : IRepository<Weapon>
    {
        Task<PagedListResponse<GetWeaponsListItemResponse>> GetAsync(GetWeaponsRequest request);
    }
}
