using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class forumupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b325af0-6aad-4f20-b0ca-aee88f1be5ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109bf94f-a1ff-4281-a942-f90b093cd39b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4212f4f-6cf7-48bf-9562-bbbd0b6bc12b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0649f927-faff-479a-a340-6d7512afa892", null, "Admin", "ADMIN" },
                    { "6c5afdea-8fce-467a-9f7a-50a065ae8706", null, "Teacher", "TEACHER" },
                    { "ba38d6ec-b78a-40f9-8630-8f11c82eec3c", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0649f927-faff-479a-a340-6d7512afa892");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c5afdea-8fce-467a-9f7a-50a065ae8706");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba38d6ec-b78a-40f9-8630-8f11c82eec3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b325af0-6aad-4f20-b0ca-aee88f1be5ba", null, "Admin", "ADMIN" },
                    { "109bf94f-a1ff-4281-a942-f90b093cd39b", null, "Student", "STUDENT" },
                    { "e4212f4f-6cf7-48bf-9562-bbbd0b6bc12b", null, "Teacher", "TEACHER" }
                });
        }
    }
}
