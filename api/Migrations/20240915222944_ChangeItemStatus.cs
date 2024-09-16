using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03875c8f-8e50-4312-9cf0-3913b34e70a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53efcfea-bde0-44ba-b8c0-f50730165597");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88a1000c-3687-4904-851e-0f1bbd7efd8a");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "syntheses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "schemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "paragraphes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "syntheses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "schemas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "paragraphes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03875c8f-8e50-4312-9cf0-3913b34e70a5", null, "Admin", "ADMIN" },
                    { "53efcfea-bde0-44ba-b8c0-f50730165597", null, "Teacher", "TEACHER" },
                    { "88a1000c-3687-4904-851e-0f1bbd7efd8a", null, "Student", "STUDENT" }
                });
        }
    }
}
