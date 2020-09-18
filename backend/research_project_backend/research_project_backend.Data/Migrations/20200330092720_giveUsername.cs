using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class giveUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "6a4ad30c-fa5f-4e32-bd73-b631d3ffada6", "AQAAAAEAACcQAAAAELt4EopwXCMbCvPBzd8eeBXKJ0mbm3oHEAjfpstNh2IqXIcXsPubfnvDjOa3GzehrQ==", "LowieVanGaal" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "80f23faf-894c-485d-a73f-a4826e3f291a", "AQAAAAEAACcQAAAAEE7VDC4wX9UTsmVy3xE5tofade4qEnD2eTOrypa07YvmaAyoC7+E+Khh5CrFPae5eg==", null });
        }
    }
}
