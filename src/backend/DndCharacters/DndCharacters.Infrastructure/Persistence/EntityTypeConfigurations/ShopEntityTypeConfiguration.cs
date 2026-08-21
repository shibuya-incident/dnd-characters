using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable(InfrastructureConfiguration.ShopTableName);

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.DisplayImage)
                .HasMaxLength(500);

            builder.Property(s => s.ShopType)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.OwnerName)
                .HasMaxLength(100);

            builder.HasData(
         new
         {
             Id = 1,
             Name = "The Arcane",
             ProfileImage = (string?)null,
             ShopType = ShopType.Bookstore,
             OwnerName = "Garrick"
         },
         new
         {
             Id = 2,
             Name = "Iron & Steel",
             ProfileImage = (string?)null,
             ShopType = ShopType.Blacksmith,
             OwnerName = "Brom"
         });
        }
    }
}
