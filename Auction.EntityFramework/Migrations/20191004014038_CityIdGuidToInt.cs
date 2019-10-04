using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class CityIdGuidToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCities_ProductCityId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductCities");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCityId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Cities_ProductCityId",
                table: "Products",
                column: "ProductCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Cities_ProductCityId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductCityId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCities", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCities_ProductCityId",
                table: "Products",
                column: "ProductCityId",
                principalTable: "ProductCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
