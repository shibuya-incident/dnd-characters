using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Characters.AddCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.CreateCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.GetCharacterById;
using DndCharacters.Application.Dtos.Characters.GetCharacters;
using DndCharacters.Application.Dtos.Characters.GetCharactersCount;
using DndCharacters.Application.Dtos.Characters.UpdateCharacter;

namespace DndCharacters.Application.Interfaces
{
    public interface ICharacterService
    {
        Task<PagedListResponse<GetCharactersListItemResponse>> GetFilteredAsync(GetCharactersRequest request, CancellationToken cancellationToken = default);
        Task<CreateCharacterResponse> CreateAsync(CreateCharacterRequest request);
        Task<GetCharacterByIdResponse> GetByIdAsync(GetCharacterByIdRequest request);
        Task DeleteAsync(DeleteCharacterRequest request);
        Task<UpdateCharacterResponse> UpdateAsync(int id, UpdateCharacterRequest request);
        Task<GetCharactersCountResponse> GetCharactersCountAsync();
        Task<AddCharacterWeaponResponse> AddCharacterWeaponAsync(int characterId, int weaponId, AddCharacterWeaponRequest request);
        Task DeleteCharacterWeaponAsync(DeleteCharacterWeaponRequest request);
    }
}
