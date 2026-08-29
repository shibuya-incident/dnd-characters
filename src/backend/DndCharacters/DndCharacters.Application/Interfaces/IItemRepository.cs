using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<GetItemsResponse> GetAsync(GetItemsRequest request);
    }
}
