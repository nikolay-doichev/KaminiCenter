using Microsoft.EntityFrameworkCore.Migrations;

namespace KaminiCenter.Data.Migrations
{
    public partial class InitialCreate_Update_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "Group_Name",
                table: "Product_Groups");

            migrationBuilder.DropColumn(
                name: "Group_Id",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "TypeOfChamber_Id",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Accessories");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Product_Groups",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Fireplace_Chambers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfChamberId1",
                table: "Fireplace_Chambers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Accessories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId1",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId1",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId1",
                principalTable: "TypeOfChambers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.DropIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Product_Groups");

            migrationBuilder.DropColumn(
                name: "TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.AddColumn<string>(
                name: "Group_Name",
                table: "Product_Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Fireplace_Chambers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Group_Id",
                table: "Fireplace_Chambers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfChamber_Id",
                table: "Fireplace_Chambers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Product_Id",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Accessories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Product_Id",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId",
                principalTable: "TypeOfChambers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
