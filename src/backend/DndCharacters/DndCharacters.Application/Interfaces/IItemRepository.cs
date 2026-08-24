using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IItemRepository
    {
        Task AddAsync(Item item);
        Task<Item?> GetByIdAsync(int id);
        Task<GetItemsResponse> GetAsync(GetItemsRequest request);
        Task UpdateAsync(Item item);
        Task Remove(Item item);
    }
}
