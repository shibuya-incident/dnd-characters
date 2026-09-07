using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<PagedListResponse<GetItemsListItemResponse>> GetAsync(GetItemsRequest request);
    }
}
