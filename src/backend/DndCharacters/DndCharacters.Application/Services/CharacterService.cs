using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Characters.AddCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.CreateCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.GetCharacterById;
using DndCharacters.Application.Dtos.Characters.GetCharacters;
using DndCharacters.Application.Dtos.Characters.GetCharactersCount;
using DndCharacters.Application.Dtos.Characters.GetCharacterWeapons;
using DndCharacters.Application.Dtos.Characters.UpdateCharacter;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using FluentValidation;

namespace DndCharacters.Application.Services
{
    public class CharacterService(ICharacterRepository characterRepository, IWeaponRepository weaponRepository) : ICharacterService
    {
        public async Task<AddCharacterWeaponResponse> AddCharacterWeaponAsync(int characterId, int weaponId, AddCharacterWeaponRequest request)
        {
            Character character = await characterRepository.GetByIdAsync(characterId)
                ?? throw new KeyNotFoundException($"Character with id {characterId} not found.");

            Weapon weapon = await weaponRepository.GetByIdAsync(weaponId)
                ?? throw new KeyNotFoundException($"Weapon with id {weaponId} not found.");

            character.AddWeapon(weapon);

            await characterRepository.UpdateAsync(character);

            return new AddCharacterWeaponResponse(
                weapon.Id,
                weapon.CharacterId,
                weapon.Name,
                weapon.Type,
                weapon.ObtainedAt,
                weapon.DisplayImageUrl);
        }

        public async Task<CreateCharacterResponse> CreateAsync(CreateCharacterRequest request)
        {
            await new CreateCharacterRequestValidator().ValidateAndThrowAsync(request);

            Character character = Character.Create(
                request.Name,
                request.Surname,
                request.Gender,
                request.Height,
                request.Age,
                request.Birthday,
                request.Lore,
                request.DisplayImageUrl,
                request.Race,
                request.Class,
                request.Pet);

            await characterRepository.AddAsync(character);

            return new CreateCharacterResponse(
                character.Id,
                character.Name,
                character.Surname,
                character.Gender,
                character.Height,
                character.Age,
                character.Birthday,
                character.Lore,
                character.DisplayImageUrl,
                character.Race,
                character.Class,
                character.Pet);
        }

        public async Task DeleteAsync(DeleteCharacterRequest request)
        {
            Character character = await characterRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Character with id {request.Id} not found.");

            await characterRepository.RemoveAsync(character);
        }

        public async Task DeleteCharacterWeaponAsync(DeleteCharacterWeaponRequest request)
        {
            Character character = await characterRepository.GetByIdAsync(request.CharacterId)
                ?? throw new KeyNotFoundException($"Shop with id {request.CharacterId} not found.");

            Weapon weapon = await weaponRepository.GetByIdAsync(request.WeaponId)
                ?? throw new KeyNotFoundException($"Weapon with id {request.WeaponId} not found.");

            character.RemoveWeapon(weapon);

            await characterRepository.UpdateAsync(character);
        }

        public async Task<GetCharacterByIdResponse> GetByIdAsync(GetCharacterByIdRequest request)
        {
            Character character = await characterRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Character with id {request.Id} not found.");

            return new GetCharacterByIdResponse(
                character.Id,
                character.Name,
                character.Surname,
                character.Gender,
                character.Height,
                character.Age,
                character.Birthday,
                character.Lore,
                character.DisplayImageUrl,
                character.Race,
                character.Class,
                character.Pet);
        }

        public async Task<GetCharactersCountResponse> GetCharactersCountAsync()
        {
            int count = await characterRepository.CountAsync();
            return new GetCharactersCountResponse(count);
        }

        public async Task<PagedListResponse<GetCharacterWeaponsListItemResponse>> GetCharacterWeaponsAsync(int id, GetCharacterWeaponsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedListResponse<GetCharactersListItemResponse>> GetFilteredAsync(GetCharactersRequest request, CancellationToken cancellationToken = default)
        {
            return await characterRepository.GetAsync(request, cancellationToken);
        }

        public async Task<UpdateCharacterResponse> UpdateAsync(int id, UpdateCharacterRequest request)
        {
            await new UpdateCharacterRequestValidator().ValidateAndThrowAsync(request);

            Character character = await characterRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Character with id {id} not found.");

            character.Name = request.Name;
            character.Surname = request.Surname;
            character.Gender = request.Gender;
            character.Height = request.Height;
            character.Age = request.Age;
            character.Birthday = request.Birthday;
            character.Lore = request.Lore;
            character.DisplayImageUrl = request.DisplayImageUrl;
            character.Race = request.Race;
            character.Class = request.Class;
            character.Pet = request.Pet;

            await characterRepository.UpdateAsync(character);

            return new UpdateCharacterResponse(
                character.Id,
                character.Name,
                character.Surname,
                character.Gender,
                character.Height,
                character.Age,
                character.Birthday,
                character.Lore,
                character.DisplayImageUrl,
                character.Race,
                character.Class,
                character.Pet);
        }
    }
}
