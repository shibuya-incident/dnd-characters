using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Dtos.Shops.UpdateShop;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        Task<GetShopsResponse> GetFilteredAsync(GetShopsRequest request);
        Task<CreateShopResponse> CreateAsync(CreateShopRequest request);
        Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request);
        Task DeleteAsync(DeleteShopRequest request);
        Task<UpdateShopResponse> UpdateAsync(int Id, UpdateShopRequest request);
        Task<GetShopItemByIdResponse> GetShopItemByIdAsync(GetShopItemByIdRequest request);
    }
}
