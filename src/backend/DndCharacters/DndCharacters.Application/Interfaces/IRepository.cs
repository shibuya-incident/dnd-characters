using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<bool> ExistAsync(int id);
        Task<int> CountAsync();
    }
}
