using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class AddSecondCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "AmountOfITPersonnel", "AmountOfPersonnel", "NameCompany" },
                values: new object[] { 2, 32, 321, "Bedrijf 2" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "CompanyId", "Number", "PostalNumber", "Street", "Township" },
                values: new object[] { 2, 2, 24, "3500", " Elfde-Liniestraat", "Hasselt" });

            migrationBuilder.InsertData(
                table: "ContactCompany",
                columns: new[] { "Id", "CompanyId", "Email", "JobTitle", "LastName", "Name", "PhoneNumber" },
                values: new object[] { 2, 2, null, "Scrum master", "Vangaal", "Lowie", "0475575757" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactCompany",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 2);
        }
    }
}
