using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ShopRepository(AppDbContext dbContext) : IShopRepository
    {
        public async Task<ShopItem?> GetShopItemAsync(int shopId, int itemId)
        {
            return await dbContext.ShopItems
                .Include(shopItem => shopItem.Shop)
                .Include(shopItem => shopItem.Item)
                .FirstOrDefaultAsync(shopItem => shopItem.Shop.Id == shopId && shopItem.Item.Id == itemId);
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
                .ThenInclude(i => i.Item)
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
