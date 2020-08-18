using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rozetka.PriceTracker.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalProductId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    OldPrice = table.Column<decimal>(nullable: false),
                    Href = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SellStatus = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistoty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistoty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistoty_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistoty_ProductId",
                table: "ProductPriceHistoty",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPriceHistoty");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
