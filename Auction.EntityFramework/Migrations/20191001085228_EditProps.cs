using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class EditProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductActive",
                table: "Products",
                newName: "ProductIsActive");

            migrationBuilder.RenameColumn(
                name: "UserAdress",
                table: "AspNetUsers",
                newName: "UserAddress");

            migrationBuilder.RenameColumn(
                name: "UserActive",
                table: "AspNetUsers",
                newName: "UserIsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductIsActive",
                table: "Products",
                newName: "ProductActive");

            migrationBuilder.RenameColumn(
                name: "UserIsActive",
                table: "AspNetUsers",
                newName: "UserActive");

            migrationBuilder.RenameColumn(
                name: "UserAddress",
                table: "AspNetUsers",
                newName: "UserAdress");
        }
    }
}
