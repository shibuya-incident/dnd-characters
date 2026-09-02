using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ShopRepository(AppDbContext dbContext) : Repository<Shop>(dbContext), IShopRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<GetShopItemByIdResponse?> GetShopItemAsync(GetShopItemByIdRequest request)
        {
            return await dbContext.ShopItems
                .Where(shopItem =>
                    shopItem.ShopId == request.ShopId &&
                    shopItem.ItemId == request.ItemId)
                .Join(
                dbContext.Items,
                shopItem => shopItem.ItemId,
                item => item.Id,
                (shopItem, item) => new GetShopItemByIdResponse(
                    shopItem.Id,
                    shopItem.ShopId,
                    shopItem.ItemId,
                    item.Name,
                    item.ItemType,
                    item.Description,
                    shopItem.Description,
                    shopItem.Price,
                    shopItem.Stock,
                    shopItem.IsOutOfStock,
                    item.DisplayImageUrl
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<GetShopItemsResponse> GetShopItemsAsync(GetShopItemsRequest request)
        {
            List<GetShopItemsListItemResponse> items = await dbContext.ShopItems
                .Where(shopItem =>
                    shopItem.ShopId == request.ShopId)
                .Join(
                dbContext.Items,
                shopItem => shopItem.ItemId,
                item => item.Id,
                (shopItem, item) => new GetShopItemsListItemResponse(
                    shopItem.Id,
                    item.Id,
                    item.Name,
                    item.DisplayImageUrl,
                    item.ItemType,
                    shopItem.Stock,
                    shopItem.Price
                ))
                .AsNoTracking()
                .ToListAsync();

            return new GetShopItemsResponse()
            {
                Items = items
            };
        }

        public async Task<PagedListResponse<GetShopsListItemResponse>> GetAsync(GetShopsRequest request, CancellationToken cancellationToken = default)
        {
            IQueryable<Shop> query = dbContext.Shops.AsNoTracking();

            //Filters

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                //query = query.Where(shop => shop.ToLower().Contains(request.Name.ToLower())));
                query = query.Where(shop => EF.Functions.ILike(shop.Name, $"%{request.Name}%"));
            }

            if (request.ShopType is not null)
            {
                query = query.Where(shop => shop.ShopType == request.ShopType);
            }

            if (request.ItemsCount is not null)
            {
                query = query.Where(shop => shop.ShopItems.Count >= request.ItemsCount);
            }

            //Count
            int totalCount = await query.CountAsync();

            //Sorting
            Expression<Func<Shop, object>> sortByExpression = GetSortByExpression(request.SortBy);
            query = query.ApplySortDirection(request.SortDirection, sortByExpression);

            //Projection & Pagination 
            List<GetShopsListItemResponse> shops = await query
                .Select(shop => new GetShopsListItemResponse(
                    shop.Id,
                    shop.Name,
                    shop.ShopType,
                    shop.DisplayImageUrl,
                    shop.ShopItems.Count
                ))
                .ApplyPagination(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedListResponse<GetShopsListItemResponse>()
            {
                Items = shops,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<Shop, object>> GetSortByExpression(GetShopsSortByRequest sortBy) => sortBy switch
        {
            GetShopsSortByRequest.Id => shop => shop.Id,
            GetShopsSortByRequest.Name => shop => shop.Name,
            GetShopsSortByRequest.ShopType => shop => shop.ShopType,
            GetShopsSortByRequest.CreatedAt => shop => shop.CreatedAt,
            GetShopsSortByRequest.OwnerName => shop => shop.OwnerName,
            _ => shop => shop.CreatedAt,
        };

        public async Task<bool> ExistAsync(int id, int itemId)
        {
            return await dbContext.ShopItems.AnyAsync(shopItem => shopItem.ShopId == id && shopItem.ItemId == itemId);
        }

        public async Task RemoveShopItem(ShopItem shopItem)
        {
            dbContext.ShopItems.Remove(shopItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
