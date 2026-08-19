using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        public Task<GetShopsResponse> GetFilteredAsync(GetShopsRequest request);
        public Task<CreateShopResponse> CreateAsync(CreateShopRequest request);
        public Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request);
    }
}
