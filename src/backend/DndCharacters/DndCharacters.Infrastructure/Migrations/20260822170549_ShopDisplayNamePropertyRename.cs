using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShopDisplayNamePropertyRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayImage",
                table: "shops",
                newName: "DisplayImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayImageUrl",
                table: "shops",
                newName: "DisplayImage");
        }
    }
}
