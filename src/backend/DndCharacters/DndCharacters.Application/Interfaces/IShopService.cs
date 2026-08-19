using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        public GetShopsResponse GetFiltered(GetShopsRequest request);
        public CreateShopResponse Create(CreateShopRequest request);
        public GetShopByIdResponse GetById(GetShopByIdRequest request);
    }
}
