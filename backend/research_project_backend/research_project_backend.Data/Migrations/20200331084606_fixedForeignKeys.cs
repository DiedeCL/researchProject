using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class fixedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment");

            migrationBuilder.DropForeignKey(
                name: "FK_IntroductionCondition_Assignments_InternshipAssignmentId",
                table: "IntroductionCondition");

            migrationBuilder.AlterColumn<int>(
                name: "InternshipAssignmentId",
                table: "IntroductionCondition",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InternshipAssignmentId",
                table: "Environment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntroductionCondition_Assignments_InternshipAssignmentId",
                table: "IntroductionCondition",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment");

            migrationBuilder.DropForeignKey(
                name: "FK_IntroductionCondition_Assignments_InternshipAssignmentId",
                table: "IntroductionCondition");

            migrationBuilder.AlterColumn<int>(
                name: "InternshipAssignmentId",
                table: "IntroductionCondition",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "InternshipAssignmentId",
                table: "Environment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Assignments_InternshipAssignmentId",
                table: "Environment",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntroductionCondition_Assignments_InternshipAssignmentId",
                table: "IntroductionCondition",
                column: "InternshipAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
