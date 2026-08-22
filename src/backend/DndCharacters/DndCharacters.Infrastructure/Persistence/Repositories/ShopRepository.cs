using DndCharacters.Application.Dtos.Shops.GetShopItemById;
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
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Shop>> GetAsync(GetShopsRequest request)
        {
            return await dbContext.Shops
                .Include(s => s.ShopItems)
                .ToListAsync();
        }

        public async Task<Shop?> GetByIdAsync(int id)
        {
            return await dbContext.Shops
                .Include(s => s.ShopItems)
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
    }
}
