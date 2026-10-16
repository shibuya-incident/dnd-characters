using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Weapons.CreateWeapon;
using DndCharacters.Application.Dtos.Weapons.DeleteWeapon;
using DndCharacters.Application.Dtos.Weapons.GetWeaponById;
using DndCharacters.Application.Dtos.Weapons.GetWeapons;
using DndCharacters.Application.Dtos.Weapons.GetWeaponsCount;
using DndCharacters.Application.Dtos.Weapons.UpdateWeapon;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using FluentValidation;

namespace DndCharacters.Application.Services
{
    public class WeaponService(IWeaponRepository weaponRepository) : IWeaponService
    {
        public async Task<CreateWeaponResponse> CreateAsync(CreateWeaponRequest request)
        {
            await new CreateWeaponRequestValidator().ValidateAndThrowAsync(request);

            Weapon weapon = Weapon.Create(
                request.Name,
                request.Description,
                request.Weight,
                request.Properties,
                request.WeaponType,
                request.DisplayImageUrl,
                request.CharacterId);

            await weaponRepository.AddAsync(weapon);

            return new CreateWeaponResponse(
                weapon.Id,
                weapon.Name,
                weapon.Description,
                weapon.Weight,
                weapon.Properties,
                weapon.Type,
                weapon.DisplayImageUrl,
                weapon.CharacterId);
        }

        public async Task DeleteAsync(DeleteWeaponRequest request)
        {
            Weapon weapon = await weaponRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Weapon with id {request.Id} not found.");

            await weaponRepository.RemoveAsync(weapon);
        }

        public async Task<GetWeaponByIdResponse> GetByIdAsync(GetWeaponByIdRequest request)
        {
            Weapon weapon = await weaponRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Weapon with id {request.Id} not found.");

            return new GetWeaponByIdResponse(
                weapon.Id,
                weapon.Name,
                weapon.Description,
                weapon.Weight,
                weapon.Properties,
                weapon.Type,
                weapon.ObtainedAt,
                weapon.DisplayImageUrl,
                weapon.CharacterId);
        }

        public async Task<PagedListResponse<GetWeaponsListItemResponse>> GetFilteredAsync(GetWeaponsRequest request)
        {
            return await weaponRepository.GetAsync(request);
        }

        public async Task<GetWeaponsCountResponse> GetWeaponsCountAsync()
        {
            int count = await weaponRepository.CountAsync();
            return new GetWeaponsCountResponse(count);
        }

        public async Task<UpdateWeaponResponse> UpdateAsync(int Id, UpdateWeaponRequest request)
        {
            await new UpdateWeaponRequestValidator().ValidateAndThrowAsync(request);

            Weapon weapon = await weaponRepository.GetByIdAsync(Id)
                ?? throw new Exception($"Weapon with id {Id} not found.");

            weapon.Name = request.Name;
            weapon.Description = request.Description;
            weapon.Weight = request.Weight;
            weapon.Properties = request.Properties;
            weapon.Type = request.Type;
            weapon.DisplayImageUrl = request.DisplayImageUrl;
            weapon.CharacterId = request.CharacterId;

            await weaponRepository.UpdateAsync(weapon);

            return new UpdateWeaponResponse(
                weapon.Id,
                weapon.Name,
                weapon.Description,
                weapon.Weight,
                weapon.Properties,
                weapon.Type,
                weapon.ObtainedAt,
                weapon.DisplayImageUrl,
                weapon.CharacterId);
        }
    }
}
