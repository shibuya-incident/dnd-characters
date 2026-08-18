using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository
    {
        public IEnumerable<Shop> Get(GetShopsRequest request);
    }
}
