using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Commons.Sorting;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Extensions;
using DndCharacters.Infrastructure.Persistence.DataModels;
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

        public async Task<PagedListResponse<GetShopItemsListItemResponse>> GetShopItemsAsync(int shopId, GetShopItemsRequest request)
        {
            var query = dbContext.ShopItems
                .Where(shopItem =>
                    shopItem.ShopId == shopId)
                .Join(
                dbContext.Items,
                shopItem => shopItem.ItemId,
                item => item.Id,
                (shopItem, item) => new ShopItemQueryModel(shopItem, item))
                .AsNoTracking();

            query = ApplyShopItemFilters(request, query);

            int totalCount = await query.CountAsync();

            query = ApplySorting(
                query,
                request.SortBy,
                request.SortDirection);

            List<GetShopItemsListItemResponse> items = await query
                .Select(x => new GetShopItemsListItemResponse(
                    x.ShopItem.Id,
                    x.Item.Id,
                    x.Item.Name,
                    x.Item.DisplayImageUrl,
                    x.Item.ItemType,
                    x.ShopItem.Stock,
                    x.ShopItem.Price))
                .ApplyPagination(request.Page, request.PageSize)
                .ToListAsync();

            return new PagedListResponse<GetShopItemsListItemResponse>()
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ShopItemQueryModel> ApplyShopItemFilters(GetShopItemsRequest request, IQueryable<ShopItemQueryModel> query)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(queryModel => EF.Functions.ILike(queryModel.Item.Name, $"%{request.Name}%"));
            }

            if (request.ItemType is not null)
            {
                query = query.Where(queryModel => queryModel.Item.ItemType == request.ItemType);
            }

            if (request.Stock is not null)
            {
                query = query.Where(queryModel => queryModel.ShopItem.Stock == request.Stock);
            }
            if (request.Price is not null)
            {
                query = query.Where(queryModel => queryModel.ShopItem.Price == request.Price);
            }

            return query;
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
            Expression<Func<Shop, object>> sortByExpression = GetSortByExpressionShops(request.SortBy);
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

        private static Expression<Func<Shop, object>> GetSortByExpressionShops(GetShopsSortByRequest sortBy) => sortBy switch
        {
            GetShopsSortByRequest.Id => shop => shop.Id,
            GetShopsSortByRequest.Name => shop => shop.Name,
            GetShopsSortByRequest.ShopType => shop => shop.ShopType,
            GetShopsSortByRequest.CreatedAt => shop => shop.CreatedAt,
            GetShopsSortByRequest.OwnerName => shop => shop.OwnerName,
            _ => shop => shop.CreatedAt
        };

        private static IQueryable<ShopItemQueryModel> ApplySorting(
            IQueryable<ShopItemQueryModel> query,
            GetShopItemsSortByRequest sortBy,
            SortDirection sortDirection)
        {
            return sortBy switch
            {
                GetShopItemsSortByRequest.Id =>
                    sortDirection == SortDirection.Desc
                        ? query.OrderByDescending(x => x.ShopItem.Id)
                        : query.OrderBy(x => x.ShopItem.Id),

                GetShopItemsSortByRequest.Name =>
                    sortDirection == SortDirection.Desc
                        ? query.OrderByDescending(x => x.Item.Name)
                        : query.OrderBy(x => x.Item.Name),

                GetShopItemsSortByRequest.ItemType =>
                    sortDirection == SortDirection.Desc
                        ? query.OrderByDescending(x => x.Item.ItemType)
                        : query.OrderBy(x => x.Item.ItemType),

                GetShopItemsSortByRequest.Stock =>
                    sortDirection == SortDirection.Desc
                        ? query.OrderByDescending(x => x.ShopItem.Stock)
                        : query.OrderBy(x => x.ShopItem.Stock),

                GetShopItemsSortByRequest.Price =>
                    sortDirection == SortDirection.Desc
                        ? query.OrderByDescending(x => x.ShopItem.Price)
                        : query.OrderBy(x => x.ShopItem.Price),

                _ => query.OrderBy(x => x.ShopItem.Id)
            };
        }

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
