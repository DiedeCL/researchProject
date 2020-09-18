using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class chengedPassw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80f23faf-894c-485d-a73f-a4826e3f291a", "AQAAAAEAACcQAAAAEE7VDC4wX9UTsmVy3xE5tofade4qEnD2eTOrypa07YvmaAyoC7+E+Khh5CrFPae5eg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05e71798-7523-42cd-9978-fd9f0a414d4d", "AQAAAAEAACcQAAAAEKzJ68dcUeaqW1H0dRJz037omuMKbkTE7HCrEAZuFDlEW+ns5y2/il9gprexz0lLRQ==" });
        }
    }
}
