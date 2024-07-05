using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "341aaa1f-a227-4035-a096-78c117ea33e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63021c91-7b00-47c4-a4a5-3605fde0f121");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3cbeef9-33f4-43f0-8bca-5ff41777539a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23e538d9-4aa5-41ec-a9f3-cacdf11b533c", null, "Student", "STUDENT" },
                    { "5ee1fced-b5c4-4afd-a505-7f8b004d9e0e", null, "Teacher", "TEACHER" },
                    { "b1b9fa10-d14d-4901-99d6-3b1144822d1f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23e538d9-4aa5-41ec-a9f3-cacdf11b533c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ee1fced-b5c4-4afd-a505-7f8b004d9e0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1b9fa10-d14d-4901-99d6-3b1144822d1f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "341aaa1f-a227-4035-a096-78c117ea33e0", null, "Student", "STUDENT" },
                    { "63021c91-7b00-47c4-a4a5-3605fde0f121", null, "Admin", "ADMIN" },
                    { "f3cbeef9-33f4-43f0-8bca-5ff41777539a", null, "Teacher", "TEACHER" }
                });
        }
    }
}
