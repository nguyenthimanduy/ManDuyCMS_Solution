using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class TenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesProduct_CategoryProductId",
                table: "Products");

            // KHÔNG Drop ProductImages vì bảng này không tồn tại

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesProduct_CategoryProductId",
                table: "Products",
                column: "CategoryProductId",
                principalTable: "CategoriesProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesProduct_CategoryProductId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesProduct_CategoryProductId",
                table: "Products",
                column: "CategoryProductId",
                principalTable: "CategoriesProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}