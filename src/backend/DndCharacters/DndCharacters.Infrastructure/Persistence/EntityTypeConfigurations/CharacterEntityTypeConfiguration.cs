using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;
using DndCharacters.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class CharacterEntityTypeConfiguration : EntityTypeConfiguration<Character>
    {
        public override void Configure(EntityTypeBuilder<Character> builder)
        {
            base.Configure(builder);

            builder.ToTable(InfrastructureConfiguration.CharacterTableName);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Surname)
                .HasMaxLength(100);

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Height)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Age)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Birthday)
                .IsRequired();

            builder.Property(x => x.Lore)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.DisplayImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.CharacterRace)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CharacterClass)
                .HasConversion<string>()
                .IsRequired();

            builder.ComplexProperty(x => x.Pet, pet =>
            {
                pet.Property<string>(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                pet.Property<string>(x => x.Species)
                    .IsRequired()
                    .HasMaxLength(100);

                pet.Property<string>(x => x.Lore)
                    .IsRequired()
                    .HasMaxLength(1000);

                pet.Property<string>(x => x.DisplayImageUrl!)
                    .HasColumnName("PetDisplayImageUrl")
                    .HasMaxLength(500)
                    .IsRequired(false);
            });

            builder.HasMany<Weapon>()
                .WithOne()
                .HasForeignKey(x => x.CharacterId)
                .IsRequired(false);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "Obi",
                    Gender = "Male",
                    Height = 180m,
                    Age = 22,
                    Birthday = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Lore = "A bisexual rogue with a lot of issues and a sssnake",
                    CharacterRace = CharacterRace.Elf,
                    CharacterClass = CharacterClass.Rogue,
                    Pet = new Pet()
                    {
                        Name = "Thomasss",
                        Species = "Snake",
                        Lore = "A venomous yet friendly snake"
                    },
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)

                });
        }
    }
}
