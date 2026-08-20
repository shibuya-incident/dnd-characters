using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Shops
{
    internal sealed class ShopRepository(AppDbContext dbContext) : IShopRepository
    {
        public async Task AddAsync(Shop shop)
        {
            await dbContext.Shops.AddAsync(shop);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Shop>> GetAsync(GetShopsRequest request)
        {
            return await dbContext.Shops.ToListAsync();
        }

        public async Task<Shop?> GetByIdAsync(int id)
        {
            return await dbContext.Shops.FirstOrDefaultAsync(s => s.Id == id);
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
