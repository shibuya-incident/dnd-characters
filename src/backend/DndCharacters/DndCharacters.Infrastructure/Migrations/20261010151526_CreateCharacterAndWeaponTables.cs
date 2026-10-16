using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateCharacterAndWeaponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Lore = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DisplayImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CharacterRace = table.Column<string>(type: "text", nullable: false),
                    CharacterClass = table.Column<string>(type: "text", nullable: false),
                    PetDisplayImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Pet_Lore = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Pet_Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Pet_Species = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    Properties = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WeaponType = table.Column<string>(type: "text", nullable: false),
                    ObtainedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisplayImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_weapons_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Age", "Birthday", "CharacterClass", "CharacterRace", "CreatedAt", "DisplayImageUrl", "Gender", "Height", "Lore", "Name", "Surname", "UpdatedAt" },
                values: new object[] { 1, 22, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rogue", "Elf", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Male", 180m, "A bisexual rogue with a lot of issues and a sssnake", "Obi", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "weapons",
                columns: new[] { "Id", "CharacterId", "CreatedAt", "Description", "DisplayImageUrl", "Name", "ObtainedAt", "Properties", "UpdatedAt", "WeaponType", "Weight" },
                values: new object[] { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wouldn't you like to know", null, "Stormbringer", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Versatile", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Longsword", 15m });

            migrationBuilder.CreateIndex(
                name: "IX_weapons_CharacterId",
                table: "weapons",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weapons");

            migrationBuilder.DropTable(
                name: "characters");
        }
    }
}
