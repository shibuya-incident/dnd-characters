using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Characters.GetCharacters;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface ICharacterRepository : IRepository<Character>
    {
        Task<PagedListResponse<GetCharactersListItemResponse>> GetAsync(GetCharactersRequest request, CancellationToken cancellationToken = default);
    }
}
