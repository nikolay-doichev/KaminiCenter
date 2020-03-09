﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace KaminiCenter.Data.Migrations
{
    public partial class updateAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Fireplace_Chambers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Fireplace_Chambers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
