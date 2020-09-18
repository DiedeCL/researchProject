﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace research_project_backend.Data.Migrations
{
    public partial class hasdataCompany3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "CompanyId", "JobTitle" },
                values: new object[] { 1, 0, "3d2e16e1-cdad-449d-a0a9-59a1c84ce964", "CompanyPromoter", "Lowie.VanGaal@bedrijf.com", false, "VanGaal", false, null, "Lowie", null, null, null, null, false, null, false, null, 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "CompanyId", "JobTitle" },
                values: new object[] { -1, 0, "49130454-db8d-49d7-b19e-9d5869cd2ce7", "CompanyPromoter", "Lowie.VanGaal@bedrijf.com", false, "VanGaal", false, null, "Lowie", null, null, null, null, false, null, false, null, 1, null });
        }
    }
}
