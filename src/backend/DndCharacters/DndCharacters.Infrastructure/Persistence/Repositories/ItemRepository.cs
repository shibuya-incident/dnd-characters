using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal sealed class ItemRepository(AppDbContext dbContext) : Repository<Item>(dbContext), IItemRepository
    {
        private readonly AppDbContext dbContext = dbContext;

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
    }
}
