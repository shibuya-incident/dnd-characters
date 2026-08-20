using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Dtos.Shops.UpdateShop;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        public Task<GetShopsResponse> GetFilteredAsync(GetShopsRequest request);
        public Task<CreateShopResponse> CreateAsync(CreateShopRequest request);
        public Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request);
        public Task DeleteAsync(DeleteShopRequest request);
        public Task<UpdateShopResponse> UpdateAsync(int Id, UpdateShopRequest request);
    }
}
