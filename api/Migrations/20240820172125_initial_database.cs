using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class initial_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "095b9313-1439-412e-84f7-a96b68a2420c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0abc338d-50ae-45c6-b2eb-1624eaadecbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b37fcf4f-1b95-45a2-9d77-608e2226b232");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dc85d5d-b68f-4ba7-a421-c9a17d48721e", null, "Teacher", "TEACHER" },
                    { "642edc8f-0628-4011-aa62-2ef70b34c9c2", null, "Student", "STUDENT" },
                    { "86b47795-da18-4c26-8fd5-4639de440714", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dc85d5d-b68f-4ba7-a421-c9a17d48721e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "642edc8f-0628-4011-aa62-2ef70b34c9c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86b47795-da18-4c26-8fd5-4639de440714");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "095b9313-1439-412e-84f7-a96b68a2420c", null, "Admin", "ADMIN" },
                    { "0abc338d-50ae-45c6-b2eb-1624eaadecbb", null, "Teacher", "TEACHER" },
                    { "b37fcf4f-1b95-45a2-9d77-608e2226b232", null, "Student", "STUDENT" }
                });
        }
    }
}
