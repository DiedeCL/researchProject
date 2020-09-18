using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class changedCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Assignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CompanyId",
                table: "Assignments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Companies_CompanyId",
                table: "Assignments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Companies_CompanyId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_CompanyId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Assignments");
        }
    }
}
