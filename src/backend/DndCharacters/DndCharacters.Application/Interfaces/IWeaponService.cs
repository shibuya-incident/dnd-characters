using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Weapons.CreateWeapon;
using DndCharacters.Application.Dtos.Weapons.DeleteWeapon;
using DndCharacters.Application.Dtos.Weapons.GetWeaponById;
using DndCharacters.Application.Dtos.Weapons.GetWeapons;
using DndCharacters.Application.Dtos.Weapons.GetWeaponsCount;
using DndCharacters.Application.Dtos.Weapons.UpdateWeapon;

namespace DndCharacters.Application.Interfaces
{
    public interface IWeaponService
    {
        Task<CreateWeaponResponse> CreateAsync(CreateWeaponRequest request);
        Task<GetWeaponByIdResponse> GetByIdAsync(GetWeaponByIdRequest request);
        Task<PagedListResponse<GetWeaponsListItemResponse>> GetFilteredAsync(GetWeaponsRequest request);
        Task<UpdateWeaponResponse> UpdateAsync(int Id, UpdateWeaponRequest request);
        Task DeleteAsync(DeleteWeaponRequest request);
        Task<GetWeaponsCountResponse> GetWeaponsCountAsync();
    }
}
