using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable(InfrastructureConfiguration.ItemTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.ItemType)
                .IsRequired()
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.DisplayImageUrl)
                .HasMaxLength(500);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "Potion of Healing",
                    Description = "A red potion that restores health.",
                    ItemType = ItemType.Potion
                },
                new
                {
                    Id = 2,
                    Name = "Longsword",
                    Description = "A reliable steel longsword.",
                    ItemType = ItemType.Weapon
                });
        }
    }
}
