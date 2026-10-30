using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Weapons.GetWeapons;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class WeaponRepository(AppDbContext dbContext) : Repository<Weapon>(dbContext), IWeaponRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<PagedListResponse<GetWeaponsListItemResponse>> GetAsync(GetWeaponsRequest request)
        {
            IQueryable<Weapon> query = dbContext.Weapons.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(weapon => EF.Functions.ILike(weapon.Name, $"%{request.Name}%"));
            }

            if (request.Type is not null)
            {
                query = query.Where(weapon => weapon.Type == request.Type);
            }

            if (request.CharacterId is not null)
            {
                query = query.Where(weapon => weapon.CharacterId == request.CharacterId);
            }

            int totalCount = await query.CountAsync();

            Expression<Func<Weapon, object>> sortByExpression = GetSortByExpression(request.SortBy);
            query = query.ApplySortDirection(request.SortDirection, sortByExpression);

            List<GetWeaponsListItemResponse> weapons = await query
                .Select(weapon => new GetWeaponsListItemResponse(
                    weapon.Id,
                    weapon.Name,
                    weapon.Type,
                    weapon.ObtainedAt,
                    weapon.CreatedAt,
                    weapon.DisplayImageUrl,
                    weapon.CharacterId
                ))
                .ApplyPagination(request.Page, request.PageSize)
                .ToListAsync();

            return new PagedListResponse<GetWeaponsListItemResponse>()
            {
                Items = weapons,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<Weapon, object>> GetSortByExpression(GetWeaponsSortByRequest sortBy) => sortBy switch
        {
            GetWeaponsSortByRequest.Id => weapon => weapon.Id,
            GetWeaponsSortByRequest.Name => weapon => weapon.Name,
            GetWeaponsSortByRequest.WeaponType => weapon => weapon.Type,
            GetWeaponsSortByRequest.CreatedAt => item => item.CreatedAt,
            _ => weapon => weapon.CreatedAt,
        };
    }
}
