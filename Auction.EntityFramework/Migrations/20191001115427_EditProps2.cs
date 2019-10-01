using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class EditProps2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryUrlName",
                table: "SubCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryUrlName",
                table: "SubCategories");
        }
    }
}
