using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class hasdataCompany4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05e71798-7523-42cd-9978-fd9f0a414d4d", "AQAAAAEAACcQAAAAEKzJ68dcUeaqW1H0dRJz037omuMKbkTE7HCrEAZuFDlEW+ns5y2/il9gprexz0lLRQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3d2e16e1-cdad-449d-a0a9-59a1c84ce964", null });
        }
    }
}
