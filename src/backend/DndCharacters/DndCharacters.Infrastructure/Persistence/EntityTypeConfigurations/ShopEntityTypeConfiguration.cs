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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DisplayImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ShopType)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.OwnerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
         new
         {
             Id = 1,
             Name = "The Arcane",
             DisplayImageUrl = "https://m.media-amazon.com/images/S/pv-target-images/211525360489f7df87f8debc7eb8c9deb14a8e3a4d57e7b532ddb8371737a12f.jpg",
             ShopType = ShopType.Bookstore,
             OwnerName = "Garrick",
             CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
         },
         new
         {
             Id = 2,
             Name = "Iron & Steel",
             DisplayImageUrl = "https://static.wikia.nocookie.net/pokemonfanon/images/1/1f/Mkmdslcndsklcndsklfsn.png/revision/latest?cb=20130530003636",
             ShopType = ShopType.Blacksmith,
             OwnerName = "Brom",
             CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
         });
        }
    }
}
