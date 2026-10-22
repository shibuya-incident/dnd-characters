using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceItemTypeWeaponForThrowable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemType",
                value: "Throwable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemType",
                value: "Weapon");
        }
    }
}
