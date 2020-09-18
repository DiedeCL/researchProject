using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class sdasdssa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, 1, "4e955c30-c972-4524-99ea-f4cb15ef8e1d", "Lowie.VanGaal@bedrijf", false, false, null, null, null, "AQAAAAEAACcQAAAAEGkKEH9HVppVAocXKaOFYTUjiT1O1GMOyCFexboE0d3fOfn1/D/eCSq7k7DUYtD1Rg==", null, false, null, false, "Lowie VanGaal" });
        }
    }
}
