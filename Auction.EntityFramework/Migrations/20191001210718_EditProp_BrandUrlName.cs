using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class EditProp_BrandUrlName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "SubCategories");

            migrationBuilder.AddColumn<string>(
                name: "BrandUrlName",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandUrlName",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "SubCategories",
                nullable: true);
        }
    }
}
