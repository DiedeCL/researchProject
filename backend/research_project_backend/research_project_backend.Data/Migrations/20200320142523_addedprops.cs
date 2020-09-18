using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class addedprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ContactCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContactCompany",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ContactCompany");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContactCompany");
        }
    }
}
