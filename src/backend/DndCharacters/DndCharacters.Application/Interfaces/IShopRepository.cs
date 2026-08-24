using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository
    {
        Task AddAsync(Shop shop);
        Task<GetShopsResponse> GetAsync(GetShopsRequest request);
        Task<Shop?> GetByIdAsync(int id);
        Task<GetShopItemByIdResponse?> GetShopItemAsync(GetShopItemByIdRequest request);
        Task<GetShopItemsResponse> GetShopItemsAsync(GetShopItemsRequest request);
        Task Remove(Shop shop);
        Task UpdateAsync(Shop shop);
    }
}
