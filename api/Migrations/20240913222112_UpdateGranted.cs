using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGranted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31a85b88-9ec6-4e87-86eb-202161af3b5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad5f867-2e90-4b92-9eaa-3c2612f226a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f804da27-8195-441c-983f-0941dcc8db30");

            migrationBuilder.AlterColumn<bool>(
                name: "Granted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0f698b9-336e-4ee9-be70-9a9fc9ceac0f", null, "Teacher", "TEACHER" },
                    { "b2ff96de-e1f0-4492-937d-7b37a4c846b9", null, "Admin", "ADMIN" },
                    { "b8406ce6-bd5b-4554-af65-f682377dd393", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0f698b9-336e-4ee9-be70-9a9fc9ceac0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2ff96de-e1f0-4492-937d-7b37a4c846b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8406ce6-bd5b-4554-af65-f682377dd393");

            migrationBuilder.AlterColumn<bool>(
                name: "Granted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31a85b88-9ec6-4e87-86eb-202161af3b5b", null, "Teacher", "TEACHER" },
                    { "bad5f867-2e90-4b92-9eaa-3c2612f226a4", null, "Admin", "ADMIN" },
                    { "f804da27-8195-441c-983f-0941dcc8db30", null, "Student", "STUDENT" }
                });
        }
    }
}
