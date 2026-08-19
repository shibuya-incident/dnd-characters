using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository
    {
        Shop Add(Shop shop);
        public IEnumerable<Shop> Get(GetShopsRequest request);
        Shop? GetById(int id);
    }
}
