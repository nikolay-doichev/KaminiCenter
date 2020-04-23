namespace KaminiCenter.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditSuggestProduts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestProducts",
                table: "SuggestProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "SuggestProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FireplaceId",
                table: "SuggestProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "SuggestProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestProducts",
                table: "SuggestProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductId",
                table: "SuggestProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestProducts",
                table: "SuggestProducts");

            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_ProductId",
                table: "SuggestProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "SuggestProducts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FireplaceId",
                table: "SuggestProducts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "SuggestProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestProducts",
                table: "SuggestProducts",
                columns: new[] { "ProductId", "FireplaceId" });
        }
    }
}
