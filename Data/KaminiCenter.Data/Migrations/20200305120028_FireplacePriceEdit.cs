﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace KaminiCenter.Data.Migrations
{
    public partial class FireplacePriceEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorductId",
                table: "Fireplace_Chambers");

            migrationBuilder.DropColumn(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Fireplace_Chambers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Fireplace_Chambers",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Fireplace_Chambers",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Fireplace_Chambers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Fireplace_Chambers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Fireplace_Chambers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "PorductId",
                table: "Fireplace_Chambers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfChamberId",
                table: "Fireplace_Chambers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
