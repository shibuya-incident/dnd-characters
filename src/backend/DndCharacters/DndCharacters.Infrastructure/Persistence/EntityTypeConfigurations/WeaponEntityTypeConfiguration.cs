using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class WeaponEntityTypeConfiguration : EntityTypeConfiguration<Weapon>
    {
        public override void Configure(EntityTypeBuilder<Weapon> builder)
        {
            base.Configure(builder);

            builder.ToTable(InfrastructureConfiguration.WeaponTableName);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Weight)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Properties)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.ObtainedAt)
                .IsRequired(false);

            builder.Property(x => x.DisplayImageUrl)
                .HasMaxLength(500);

            builder.HasOne<Character>()
                .WithMany(x => x.Weapons)
                .HasForeignKey(x => x.CharacterId)
                .IsRequired(false);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "Stormbringer",
                    Description = "Wouldn't you like to know",
                    Weight = 15m,
                    Properties = "Versatile",
                    Type = WeaponType.Longsword,
                    ObtainedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CharacterId = 1
                });
        }
    }
}
