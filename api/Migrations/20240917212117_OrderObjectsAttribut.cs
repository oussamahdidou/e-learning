using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class OrderObjectsAttribut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "715502e8-e600-43ba-9e6c-c9da073f0d39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6b86d5e-ae71-48d6-a363-064e4285f3c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa7b8ad7-6b27-4ef4-9671-61578d8309c0");

            migrationBuilder.AddColumn<int>(
                name: "ObjetNumber",
                table: "videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjetNumber",
                table: "syntheses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjetNumber",
                table: "schemas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjetNumber",
                table: "paragraphes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e1f6637-9d2d-4746-892f-cc4074e1f45e", null, "Student", "STUDENT" },
                    { "df535842-f054-46a6-9c79-6d5188639061", null, "Teacher", "TEACHER" },
                    { "ef1dfa4d-4e79-41d6-9fbf-68cba02e7d09", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e1f6637-9d2d-4746-892f-cc4074e1f45e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df535842-f054-46a6-9c79-6d5188639061");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef1dfa4d-4e79-41d6-9fbf-68cba02e7d09");

            migrationBuilder.DropColumn(
                name: "ObjetNumber",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "ObjetNumber",
                table: "syntheses");

            migrationBuilder.DropColumn(
                name: "ObjetNumber",
                table: "schemas");

            migrationBuilder.DropColumn(
                name: "ObjetNumber",
                table: "paragraphes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "715502e8-e600-43ba-9e6c-c9da073f0d39", null, "Teacher", "TEACHER" },
                    { "a6b86d5e-ae71-48d6-a363-064e4285f3c0", null, "Student", "STUDENT" },
                    { "aa7b8ad7-6b27-4ef4-9671-61578d8309c0", null, "Admin", "ADMIN" }
                });
        }
    }
}
