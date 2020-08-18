using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rozetka.PriceTracker.EntityFramework.Migrations
{
    public partial class additionalprices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAdditionalPrices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ExternalId = table.Column<long>(nullable: false),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionalPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdditionalPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalPrices_ProductId",
                table: "ProductAdditionalPrices",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdditionalPrices");
        }
    }
}
