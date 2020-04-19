namespace KaminiCenter.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditFirechamberEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FireplaceId",
                table: "SuggestProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fireplace_ChamberId",
                table: "SuggestProducts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_Fireplace_ChamberId",
                table: "SuggestProducts",
                column: "Fireplace_ChamberId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestProducts_Fireplace_Chambers_Fireplace_ChamberId",
                table: "SuggestProducts",
                column: "Fireplace_ChamberId",
                principalTable: "Fireplace_Chambers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuggestProducts_Fireplace_Chambers_Fireplace_ChamberId",
                table: "SuggestProducts");

            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_Fireplace_ChamberId",
                table: "SuggestProducts");

            migrationBuilder.DropColumn(
                name: "FireplaceId",
                table: "SuggestProducts");

            migrationBuilder.DropColumn(
                name: "Fireplace_ChamberId",
                table: "SuggestProducts");
        }
    }
}
