using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ShopRepository(AppDbContext dbContext) : IShopRepository
    {
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
                    shopItem.Stock == 0,
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

        public async Task<GetShopsResponse> GetAsync(GetShopsRequest request)
        {
            IQueryable<Shop> query = dbContext.Shops.AsNoTracking();

            if (request.ItemCount is not null)
            {
                query = query.Where(shop => shop.ShopItems.Count >= request.ItemCount);
            }

            List<GetShopsListItemResponse> shops = await query
                .Select(shop => new GetShopsListItemResponse(
                    shop.Id,
                    shop.Name,
                    shop.ShopType,
                    shop.DisplayImageUrl,
                    shop.ShopItems.Count
                ))
                .ToListAsync();

            return new GetShopsResponse()
            {
                Shops = shops
            };
        }

        public async Task<Shop?> GetByIdAsync(int id)
        {

            return await dbContext.Shops
                .Include(shop => shop.ShopItems)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Shop shop)
        {
            await dbContext.Shops.AddAsync(shop);
            await dbContext.SaveChangesAsync();
        }

        public async Task Remove(Shop shop)
        {
            dbContext.Shops.Remove(shop);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shop shop)
        {
            dbContext.Shops.Update(shop);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int id, int itemId)
        {
            return await dbContext.ShopItems.AnyAsync(shopItem => shopItem.ShopId == id && shopItem.ItemId == itemId);
        }
    }
}
