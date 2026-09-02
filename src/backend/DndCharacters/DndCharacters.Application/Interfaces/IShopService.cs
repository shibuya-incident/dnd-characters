using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Shops.AddShopItem;
using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.DeleteShopItem;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Dtos.Shops.UpdateShop;
using DndCharacters.Application.Dtos.Shops.UpdateShopItem;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        Task<PagedListResponse<GetShopsListItemResponse>> GetFilteredAsync(GetShopsRequest request);
        Task<CreateShopResponse> CreateAsync(CreateShopRequest request);
        Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request);
        Task DeleteAsync(DeleteShopRequest request);
        Task<UpdateShopResponse> UpdateAsync(int id, UpdateShopRequest request);
        Task<GetShopItemByIdResponse> GetShopItemByIdAsync(GetShopItemByIdRequest request);
        Task<GetShopItemsResponse> GetShopItemsAsync(GetShopItemsRequest request);
        Task<AddShopItemResponse> AddShopItemAsync(int shopId, int itemId, AddShopItemRequest request);
        Task<UpdateShopItemResponse> UpdateShopItemAsync(int shopId, int itemId, UpdateShopItemRequest request);
        Task DeleteShopItemAsync(DeleteShopItemRequest request);
    }
}
