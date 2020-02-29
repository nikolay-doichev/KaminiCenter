using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KaminiCenter.Data.Migrations
{
    public partial class MakeTypeOfChamber_Enum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.DropTable(
                name: "TypeOfChambers");

            migrationBuilder.DropIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "TypeOfChamberId1",
                table: "Fireplace_Chambers");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfChamber",
                table: "Fireplace_Chambers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfChamber",
                table: "Fireplace_Chambers");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfChamberId1",
                table: "Fireplace_Chambers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeOfChambers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfChambers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fireplace_Chambers_TypeOfChamberId1",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId1");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfChambers_IsDeleted",
                table: "TypeOfChambers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireplace_Chambers_TypeOfChambers_TypeOfChamberId1",
                table: "Fireplace_Chambers",
                column: "TypeOfChamberId1",
                principalTable: "TypeOfChambers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
