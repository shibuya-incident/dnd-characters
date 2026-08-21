using DndCharacters.Domain.Entities;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class ShopItemEntityTypeConfiguration : IEntityTypeConfiguration<ShopItem>
    {
        public void Configure(EntityTypeBuilder<ShopItem> builder)
        {
            builder.ToTable(InfrastructureConfiguration.ShopItemTableName);
            builder.Property(x => x.Price)
              .HasPrecision(10, 2)
              .IsRequired();

            builder.Property(x => x.Stock)
                .IsRequired();

            builder.Ignore(x => x.IsOutOfStock);

            builder.HasOne(x => x.Shop)
                .WithMany(x => x.ShopItems)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Item)
                .WithMany(x => x.ShopItems)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
          new
          {
              Id = 1,
              ShopId = 1,
              ItemId = 1,
              Price = 50m,
              Stock = 10
          },
           new
           {
               Id = 2,
               ShopId = 1,
               ItemId = 2,
               Price = 65m,
               Stock = 999
           },
          new
          {
              Id = 3,
              ShopId = 2,
              ItemId = 2,
              Price = 3m,
              Stock = 5
          });
        }
    }
}
