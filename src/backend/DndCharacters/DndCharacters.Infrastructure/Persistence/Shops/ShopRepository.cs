using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Infrastructure.Persistence.Shops
{
    internal sealed class ShopRepository : IShopRepository
    {
        public Shop Add(Shop shop)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shop> Get(GetShopsRequest request)
        {
            throw new NotImplementedException();
        }

        public Shop? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
