using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository : IRepository<Shop>
    {
        Task<PagedListResponse<GetShopsListItemResponse>> GetAsync(GetShopsRequest request);
        Task<GetShopItemByIdResponse?> GetShopItemAsync(GetShopItemByIdRequest request);
        Task<GetShopItemsResponse> GetShopItemsAsync(GetShopItemsRequest request);
        Task<bool> ExistAsync(int id, int itemId);
        Task RemoveShopItem(ShopItem shopItem);
    }
}
