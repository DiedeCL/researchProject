using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class hasdataCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Address_AddressCompanyAddressId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressCompanyAddressId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddressCompanyAddressId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Address",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "AmountOfITPersonnel", "AmountOfPersonnel", "NameCompany" },
                values: new object[] { 1, 32, 321, "PXL" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "CompanyId", "Number", "PostalNumber", "Street", "Township" },
                values: new object[] { 1, 1, 24, "3500", " Elfde-Liniestraat", "Hasselt" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "CompanyId", "JobTitle" },
                values: new object[] { 1, 0, "b3c2b65f-8df6-435b-b18a-611c4d3686af", "CompanyPromoter", "Lowie.VanGaal@bedrijf.com", false, "VanGaal", false, null, "Lowie", null, null, null, null, false, null, false, null, 1, null });

            migrationBuilder.InsertData(
                table: "ContactCompany",
                columns: new[] { "Id", "CompanyId", "JobTitle", "LastName", "Name", "PhoneNumber" },
                values: new object[] { 1, 1, "Lector", "Vangaal", "Lowie", "0475575757" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CompanyId",
                table: "Address",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Companies_CompanyId",
                table: "Address",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Companies_CompanyId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_CompanyId",
                table: "Address");

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContactCompany",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressCompanyAddressId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressCompanyAddressId",
                table: "Companies",
                column: "AddressCompanyAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Address_AddressCompanyAddressId",
                table: "Companies",
                column: "AddressCompanyAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
