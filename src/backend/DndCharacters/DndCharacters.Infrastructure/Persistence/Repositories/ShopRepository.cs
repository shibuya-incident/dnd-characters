using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Commons.Sorting;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using DndCharacters.Infrastructure.Persistence.QueryModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ShopRepository(AppDbContext dbContext)
        : Repository<Shop>(dbContext), IShopRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<GetShopItemByIdResponse?> GetShopItemAsync(
            GetShopItemByIdRequest request)
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

        public async Task<PagedListResponse<GetShopItemsListItemResponse>> GetShopItemsAsync(
        int shopId,
        GetShopItemsRequest request)
        {
            IQueryable<ShopItemQueryModel> query = dbContext.ShopItems
                .Where(shopItem => shopItem.ShopId == shopId)
                .Join(
                    dbContext.Items,
                    shopItem => shopItem.ItemId,
                    item => item.Id,
                    (shopItem, item) => new ShopItemQueryModel
                    {
                        Id = shopItem.Id,
                        ItemId = item.Id,
                        Name = item.Name,
                        DisplayImageUrl = item.DisplayImageUrl,
                        ItemType = item.ItemType,
                        Stock = shopItem.Stock,
                        Price = shopItem.Price
                    })
                .AsNoTracking();

            query = ApplyShopItemFilters(request, query);

            int totalCount = await query.CountAsync();

            query = ApplyShopItemSorting(
                query,
                request.SortBy,
                request.SortDirection);

            List<GetShopItemsListItemResponse> items = await query
                .ApplyPagination(
                    request.Page,
                    request.PageSize)
                .Select(x => new GetShopItemsListItemResponse(
                    x.Id,
                    x.ItemId,
                    x.Name,
                    x.DisplayImageUrl,
                    x.ItemType,
                    x.Stock,
                    x.Price))
                .ToListAsync();

            return new PagedListResponse<GetShopItemsListItemResponse>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
        private static IQueryable<ShopItemQueryModel> ApplyShopItemFilters(
          GetShopItemsRequest request,
          IQueryable<ShopItemQueryModel> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x =>
                    EF.Functions.ILike(
                        x.Name,
                        $"%{request.Name}%"));
            }

            if (request.ItemType is not null)
            {
                query = query.Where(x =>
                    x.ItemType == request.ItemType);
            }

            if (request.Stock is not null)
            {
                query = query.Where(x =>
                    x.Stock == request.Stock);
            }

            if (request.Price is not null)
            {
                query = query.Where(x =>
                    x.Price == request.Price);
            }

            return query;
        }
        private static IQueryable<ShopItemQueryModel> ApplyShopItemSorting(
            IQueryable<ShopItemQueryModel> query,
            GetShopItemsSortByRequest sortBy,
            SortDirection sortDirection)
        {
            return sortBy switch
            {
                GetShopItemsSortByRequest.Id => query.ApplySortDirection(sortDirection, x => x.Id),
                GetShopItemsSortByRequest.Name => query.ApplySortDirection(sortDirection, x => x.Name),
                GetShopItemsSortByRequest.ItemType => query.ApplySortDirection(sortDirection, x => x.ItemType),
                GetShopItemsSortByRequest.Stock => query.ApplySortDirection(sortDirection, x => x.Stock),
                GetShopItemsSortByRequest.Price => query.ApplySortDirection(sortDirection, x => x.Price),
                _ => query.ApplySortDirection(sortDirection, x => x.Id),
            };
        }

        public async Task<PagedListResponse<GetShopsListItemResponse>> GetAsync(
            GetShopsRequest request,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Shop> query = dbContext.Shops.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(shop =>
                    EF.Functions.ILike(
                        shop.Name,
                        $"%{request.Name}%"));
            }

            if (request.ShopType is not null)
            {
                query = query.Where(shop =>
                    shop.ShopType == request.ShopType);
            }

            if (request.ItemsCount is not null)
            {
                query = query.Where(shop =>
                    shop.ShopItems.Count >= request.ItemsCount);
            }

            int totalCount = await query.CountAsync();

            Expression<Func<Shop, object>> sortByExpression =
                GetSortByExpressionShops(request.SortBy);

            query = query.ApplySortDirection(
                request.SortDirection,
                sortByExpression);

            List<GetShopsListItemResponse> shops = await query
                .Select(shop => new GetShopsListItemResponse(
                    shop.Id,
                    shop.Name,
                    shop.ShopType,
                    shop.DisplayImageUrl,
                    shop.ShopItems.Count
                ))
                .ApplyPagination(
                    request.Page,
                    request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedListResponse<GetShopsListItemResponse>
            {
                Items = shops,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<Shop, object>>
            GetSortByExpressionShops(
                GetShopsSortByRequest sortBy) =>
            sortBy switch
            {
                GetShopsSortByRequest.Id =>
                    shop => shop.Id,

                GetShopsSortByRequest.Name =>
                    shop => shop.Name,

                GetShopsSortByRequest.ShopType =>
                    shop => shop.ShopType,

                GetShopsSortByRequest.CreatedAt =>
                    shop => shop.CreatedAt,

                GetShopsSortByRequest.OwnerName =>
                    shop => shop.OwnerName,

                _ =>
                    shop => shop.CreatedAt
            };

        public async Task<bool> ExistAsync(
            int id,
            int itemId)
        {
            return await dbContext.ShopItems.AnyAsync(
                shopItem =>
                    shopItem.ShopId == id &&
                    shopItem.ItemId == itemId);
        }

        public async Task RemoveShopItem(
            ShopItem shopItem)
        {
            dbContext.ShopItems.Remove(shopItem);

            await dbContext.SaveChangesAsync();
        }
    }
}

