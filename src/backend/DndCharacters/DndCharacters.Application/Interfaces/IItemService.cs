using DndCharacters.Application.Dtos.Items.CreateItem;
using DndCharacters.Application.Dtos.Items.DeleteItem;
using DndCharacters.Application.Dtos.Items.GetItemById;
using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Dtos.Items.UpdateItem;

namespace DndCharacters.Application.Interfaces
{
    public interface IItemService
    {
        Task<CreateItemResponse> CreateAsync(CreateItemRequest request);
        Task<GetItemByIdResponse> GetByIdAsync(GetItemByIdRequest request);
        Task<GetItemsResponse> GetFilteredAsync(GetItemsRequest request);
        Task<UpdateItemResponse> UpdateAsync(int Id, UpdateItemRequest request);
        Task DeleteAsync(DeleteItemRequest request);
    }
}
