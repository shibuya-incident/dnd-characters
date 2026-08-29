using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence.Repositories
{
    internal class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> dbSet = dbContext.Set<T>();

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await dbSet.AnyAsync(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
