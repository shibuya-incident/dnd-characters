using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DndCharacters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShopItemRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_shops_ShopId",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_ShopId",
                table: "items");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "items");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "items");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "items");

            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "shops",
                newName: "DisplayImage");

            migrationBuilder.AddColumn<string>(
                name: "DisplayImageUrl",
                table: "items",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shop_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ShopId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shop_items_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_items_shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DisplayImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 2,
                column: "DisplayImageUrl",
                value: null);

            migrationBuilder.InsertData(
                table: "shop_items",
                columns: new[] { "Id", "Description", "ItemId", "Price", "ShopId", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, 1, 50m, 1, 10, null },
                    { 2, null, 2, 65m, 1, 999, null },
                    { 3, null, 2, 3m, 2, 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_shop_items_ItemId",
                table: "shop_items",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_items_ShopId",
                table: "shop_items",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop_items");

            migrationBuilder.DropColumn(
                name: "DisplayImageUrl",
                table: "items");

            migrationBuilder.RenameColumn(
                name: "DisplayImage",
                table: "shops",
                newName: "ProfileImage");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "items",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "ShopId", "Stock" },
                values: new object[] { 50m, 1, 10 });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "ShopId", "Stock" },
                values: new object[] { 75m, 2, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_items_ShopId",
                table: "items",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_items_shops_ShopId",
                table: "items",
                column: "ShopId",
                principalTable: "shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
