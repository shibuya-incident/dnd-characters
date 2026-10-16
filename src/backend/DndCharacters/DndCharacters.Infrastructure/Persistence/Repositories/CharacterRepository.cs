using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Characters.GetCharacters;
using DndCharacters.Application.Dtos.Characters.GetCharacterWeapons;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class CharacterRepository(AppDbContext dbContext) : Repository<Character>(dbContext), ICharacterRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<PagedListResponse<GetCharactersListItemResponse>> GetAsync(
            GetCharactersRequest request,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Character> query = dbContext.Characters.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(character =>
                    EF.Functions.ILike(
                        character.Name,
                        $"%{request.Name}%"));
            }

            if (request.CharacterRace is not null)
            {
                query = query.Where(character =>
                    character.CharacterRace == request.CharacterRace);
            }

            if (request.CharacterClass is not null)
            {
                query = query.Where(character =>
                    character.CharacterClass == request.CharacterClass);
            }

            int totalCount = await query.CountAsync();

            Expression<Func<Character, object>> sortByExpression =
                GetSortByExpressionCharacters(request.SortBy);

            query = query.ApplySortDirection(
                request.SortDirection,
                sortByExpression);

            List<GetCharactersListItemResponse> characters = await query
                .Select(character => new GetCharactersListItemResponse(
                    character.Id,
                    character.Name,
                    character.CharacterRace,
                    character.CharacterClass,
                    character.DisplayImageUrl
                ))
                .ApplyPagination(
                    request.Page,
                    request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedListResponse<GetCharactersListItemResponse>
            {
                Items = characters,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<Character, object>>
            GetSortByExpressionCharacters(
                GetCharactersSortByRequest sortBy) =>
            sortBy switch
            {
                GetCharactersSortByRequest.Id =>
                    character => character.Id,

                GetCharactersSortByRequest.Name =>
                    character => character.Name,

                GetCharactersSortByRequest.CharacterRace =>
                    character => character.CharacterRace,

                GetCharactersSortByRequest.CharacterClass =>
                    character => character.CharacterClass,

                GetCharactersSortByRequest.UpdatedAt =>
                    character => character.UpdatedAt,

                _ =>
                    character => character.CreatedAt
            };

        public Task<PagedListResponse<GetCharacterWeaponsListItemResponse>> GetCharacterWeaponsAsync(int id, GetCharacterWeaponsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCharacterWeapon(Weapon weapon)
        {
            throw new NotImplementedException();
        }
    }
}
