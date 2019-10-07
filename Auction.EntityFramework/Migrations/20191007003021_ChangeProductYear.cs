using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.EntityFramework.Migrations
{
    public partial class ChangeProductYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.AlterColumn<int>(
                name: "ProductYear",
                table: "Products",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProductYear",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

      
        }
    }
}
