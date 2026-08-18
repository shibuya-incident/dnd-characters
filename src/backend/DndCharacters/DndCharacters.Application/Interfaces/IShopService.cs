using DndCharacters.Application.Dtos.Shops.GetShops;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopService
    {
        public GetShopsResponse GetShops(GetShopsRequest request);
    }
}
