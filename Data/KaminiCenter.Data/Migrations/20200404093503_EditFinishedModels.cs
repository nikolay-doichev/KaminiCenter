namespace KaminiCenter.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditFinishedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Finished_Models_Finished_ModelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Finished_ModelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Finished_ModelId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Finished_ModelId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Finished_ModelId",
                table: "Comments",
                column: "Finished_ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Finished_Models_Finished_ModelId",
                table: "Comments",
                column: "Finished_ModelId",
                principalTable: "Finished_Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
