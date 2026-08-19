using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShopDefaultDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "shops",
                columns: new[] { "Id", "Name", "OwnerName", "ProfileImage", "ShopType", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "The Arcane", "Garrick", null, "Bookstore", null },
                    { 2, "Iron & Steel", "Brom", null, "Blacksmith", null }
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "Id", "Description", "ItemType", "Name", "Price", "ShopId", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "A red potion that restores health.", "Potion", "Potion of Healing", 50m, 1, 10, null },
                    { 2, "A reliable steel longsword.", "Weapon", "Longsword", 75m, 2, 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
