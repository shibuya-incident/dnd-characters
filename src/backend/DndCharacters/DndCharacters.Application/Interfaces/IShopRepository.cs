using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository
    {
        Task AddAsync(Shop shop);
        public Task<ICollection<Shop>> GetAsync(GetShopsRequest request);
        Task<Shop?> GetByIdAsync(int id);
    }
}
