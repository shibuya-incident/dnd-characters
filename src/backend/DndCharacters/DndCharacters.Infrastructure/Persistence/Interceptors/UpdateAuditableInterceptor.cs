using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DndCharacters.Infrastructure.Persistence.Interceptors
{
    internal sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
                UpdateAuditableEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateAuditableEntities(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries<Entity>().ToList();

            foreach (EntityEntry<Entity> entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    SetCurrentPropertyValue(entry, nameof(Entity.CreatedAt), utcNow);
                }

                if (entry.State == EntityState.Modified)
                {
                    SetCurrentPropertyValue(entry, nameof(Entity.UpdatedAt), utcNow);
                }
            }

            static void SetCurrentPropertyValue(
                EntityEntry entry,
                string propertyName,
                DateTime utcNow) =>
                entry.Property(propertyName).CurrentValue = utcNow;
        }
    }
}