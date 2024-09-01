using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class initup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e5ef71b-fcac-45ea-ae5c-465a2d5b935b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936910fa-3ca7-4e41-ba7d-ac0fea7169fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2f125f4-fbdf-4e92-a0e3-5e6e6ac20753");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2be08992-59ca-4992-a210-8e05848e16d5", null, "Student", "STUDENT" },
                    { "9c0715a3-098f-4c8f-a381-c6f466eb686b", null, "Teacher", "TEACHER" },
                    { "f0d8397d-c431-4f8f-83c6-cfe46da07be3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2be08992-59ca-4992-a210-8e05848e16d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c0715a3-098f-4c8f-a381-c6f466eb686b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0d8397d-c431-4f8f-83c6-cfe46da07be3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e5ef71b-fcac-45ea-ae5c-465a2d5b935b", null, "Teacher", "TEACHER" },
                    { "936910fa-3ca7-4e41-ba7d-ac0fea7169fe", null, "Student", "STUDENT" },
                    { "e2f125f4-fbdf-4e92-a0e3-5e6e6ac20753", null, "Admin", "ADMIN" }
                });
        }
    }
}
