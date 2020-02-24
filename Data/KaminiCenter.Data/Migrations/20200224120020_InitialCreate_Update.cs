using Microsoft.EntityFrameworkCore.Migrations;

namespace KaminiCenter.Data.Migrations
{
    public partial class InitialCreate_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Accessories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product_Id",
                table: "Accessories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_ProductId",
                table: "Accessories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_Products_ProductId",
                table: "Accessories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId",
                principalTable: "TypeOfChambers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_Products_ProductId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropIndex(
                name: "IX_Accessories_ProductId",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Accessories");
        }
    }
}
