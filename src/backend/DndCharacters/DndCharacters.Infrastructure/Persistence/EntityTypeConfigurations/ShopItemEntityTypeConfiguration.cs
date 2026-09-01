using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class ShopItemEntityTypeConfiguration : EntityTypeConfiguration<ShopItem>
    {
        public override void Configure(EntityTypeBuilder<ShopItem> builder)
        {
            base.Configure(builder);

            builder.ToTable(InfrastructureConfiguration.ShopItemTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
              .HasPrecision(10, 2)
              .IsRequired();

            builder.Property(x => x.Stock)
                .IsRequired();

            builder.Ignore(x => x.IsOutOfStock);

            builder.HasOne<Shop>()
                .WithMany(x => x.ShopItems)
                .HasForeignKey(x => x.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Item>()
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
          new
          {
              Id = 1,
              ShopId = 1,
              ItemId = 1,
              Price = 50m,
              Stock = 10,
              CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          },
           new
           {
               Id = 2,
               ShopId = 1,
               ItemId = 2,
               Price = 65m,
               Stock = 999,
               CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
               UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
           },
          new
          {
              Id = 3,
              ShopId = 2,
              ItemId = 2,
              Price = 3m,
              Stock = 5,
              CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
              UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          });
        }
    }
}
