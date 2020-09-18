using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class AddEnvironments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Environment",
                table: "Environment");

            migrationBuilder.RenameTable(
                name: "Environment",
                newName: "Environments");

            migrationBuilder.RenameIndex(
                name: "IX_Environment_InternshipAssignmentId",
                table: "Environments",
                newName: "IX_Environments_InternshipAssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Environments",
                table: "Environments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Environments_Assignments_InternshipAssignmentId",
                table: "Environments",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environments_Assignments_InternshipAssignmentId",
                table: "Environments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Environments",
                table: "Environments");

            migrationBuilder.RenameTable(
                name: "Environments",
                newName: "Environment");

            migrationBuilder.RenameIndex(
                name: "IX_Environments_InternshipAssignmentId",
                table: "Environment",
                newName: "IX_Environment_InternshipAssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Environment",
                table: "Environment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
