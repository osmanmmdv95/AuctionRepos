using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class AddNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UserActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserAdress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDetail",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImageUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPhoneNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryUrlName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    SubCategoryName = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductActive = table.Column<bool>(nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    ActiveDateTime = table.Column<DateTime>(nullable: false),
                    IsItSold = table.Column<bool>(nullable: false),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    ProductYear = table.Column<DateTime>(nullable: false),
                    ProductKm = table.Column<decimal>(nullable: false),
                    ProductMinPrice = table.Column<decimal>(nullable: false),
                    ProductMaxPrice = table.Column<decimal>(nullable: false),
                    ProductGearType = table.Column<string>(nullable: true),
                    ProductFuelType = table.Column<string>(nullable: true),
                    ProductImageUrl = table.Column<string>(nullable: true),
                    ProductDetail = table.Column<string>(nullable: true),
                    ProductCityId = table.Column<int>(nullable: false),
                    HighestBidderUserId = table.Column<string>(nullable: true),
                    AddProductUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_AddProductUserId",
                        column: x => x.AddProductUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_HighestBidderUserId",
                        column: x => x.HighestBidderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCities_ProductCityId",
                        column: x => x.ProductCityId,
                        principalTable: "ProductCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    BrandName = table.Column<string>(nullable: true),
                    SubCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SubCategoryId",
                table: "Brands",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AddProductUserId",
                table: "Products",
                column: "AddProductUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HighestBidderUserId",
                table: "Products",
                column: "HighestBidderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCityId",
                table: "Products",
                column: "ProductCityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "ProductCities");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "UserActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAdress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserDetail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPhoneNumber",
                table: "AspNetUsers");
        }
    }
}
