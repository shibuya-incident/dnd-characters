using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeaponType",
                table: "weapons",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CharacterRace",
                table: "characters",
                newName: "Race");

            migrationBuilder.RenameColumn(
                name: "CharacterClass",
                table: "characters",
                newName: "Class");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "weapons",
                newName: "WeaponType");

            migrationBuilder.RenameColumn(
                name: "Race",
                table: "characters",
                newName: "CharacterRace");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "characters",
                newName: "CharacterClass");
        }
    }
}
