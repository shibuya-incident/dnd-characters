using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ItemRepository(AppDbContext dbContext) : IItemRepository
    {
        public async Task AddAsync(Item item)
        {
            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task<GetItemsResponse> GetAsync(GetItemsRequest request)
        {
            IQueryable<Item> query = dbContext.Items.AsNoTracking();

            List<GetItemsListItemResponse> items = await query
                .Select(item => new GetItemsListItemResponse(
                    item.Id,
                    item.Name,
                    item.ItemType,
                    item.DisplayImageUrl
                ))
                .ToListAsync();

            return new GetItemsResponse()
            {
                Items = items
            };
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task Remove(Item item)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            dbContext.Items.Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
