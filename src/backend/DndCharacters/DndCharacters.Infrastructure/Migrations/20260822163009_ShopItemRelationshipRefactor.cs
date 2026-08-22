using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShopItemRelationshipRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShopType",
                table: "shops",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayImageUrl",
                table: "items",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 1,
                column: "DisplayImage",
                value: "https://m.media-amazon.com/images/S/pv-target-images/211525360489f7df87f8debc7eb8c9deb14a8e3a4d57e7b532ddb8371737a12f.jpg");

            migrationBuilder.UpdateData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 2,
                column: "DisplayImage",
                value: "https://static.wikia.nocookie.net/pokemonfanon/images/1/1f/Mkmdslcndsklcndsklfsn.png/revision/latest?cb=20130530003636");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShopType",
                table: "shops",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayImageUrl",
                table: "items",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 1,
                column: "DisplayImage",
                value: null);

            migrationBuilder.UpdateData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 2,
                column: "DisplayImage",
                value: null);
        }
    }
}
