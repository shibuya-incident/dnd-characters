using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ItemRepository(AppDbContext dbContext) : Repository<Item>(dbContext), IItemRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<PagedListResponse<GetItemsListItemResponse>> GetAsync(GetItemsRequest request)
        {
            IQueryable<Item> query = dbContext.Items.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(item => EF.Functions.ILike(item.Name, $"%{request.Name}%"));
            }

            if (request.ItemType is not null)
            {
                query = query.Where(item => item.ItemType == request.ItemType);
            }

            int totalCount = await query.CountAsync();

            Expression<Func<Item, object>> sortByExpression = GetSortByExpression(request.SortBy);
            query = query.ApplySortDirection(request.SortDirection, sortByExpression);

            List<GetItemsListItemResponse> items = await query
                .Select(item => new GetItemsListItemResponse(
                    item.Id,
                    item.Name,
                    item.ItemType,
                    item.DisplayImageUrl
                ))
                .ApplyPagination(request.Page, request.PageSize)
                .ToListAsync();

            return new PagedListResponse<GetItemsListItemResponse>()
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<Item, object>> GetSortByExpression(GetItemsSortByRequest sortBy) => sortBy switch
        {
            GetItemsSortByRequest.Id => item => item.Id,
            GetItemsSortByRequest.Name => item => item.Name,
            GetItemsSortByRequest.ItemType => item => item.ItemType,
            GetItemsSortByRequest.CreatedAt => item => item.CreatedAt,
            _ => item => item.CreatedAt,
        };
    }
}
