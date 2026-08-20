using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopRepository
    {
        Task AddAsync(Shop shop);
        Task<ICollection<Shop>> GetAsync(GetShopsRequest request);
        Task<Shop?> GetByIdAsync(int id);
        Task Remove(Shop shop);
        Task UpdateAsync(Shop shop);
    }
}
