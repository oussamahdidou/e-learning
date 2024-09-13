using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class grantedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4957c14c-85bc-440a-8a01-bfb9e1c7224f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "690a8c72-b74d-4c12-be58-b1da3c655f56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f25f5098-cbfb-4b93-a21d-f1d51418203e");

            migrationBuilder.DropColumn(
                name: "Teacher_Etablissement",
                table: "AspNetUsers");

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
                    { "622ed319-a6a0-4ffb-b373-71a23be553a8", null, "Admin", "ADMIN" },
                    { "87f5d64c-c0ca-4b37-8f2e-7669a5ab10e1", null, "Teacher", "TEACHER" },
                    { "9be202a3-9898-4e63-8938-9c19dfc99a76", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "622ed319-a6a0-4ffb-b373-71a23be553a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87f5d64c-c0ca-4b37-8f2e-7669a5ab10e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9be202a3-9898-4e63-8938-9c19dfc99a76");

            migrationBuilder.AlterColumn<bool>(
                name: "Granted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Teacher_Etablissement",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4957c14c-85bc-440a-8a01-bfb9e1c7224f", null, "Teacher", "TEACHER" },
                    { "690a8c72-b74d-4c12-be58-b1da3c655f56", null, "Admin", "ADMIN" },
                    { "f25f5098-cbfb-4b93-a21d-f1d51418203e", null, "Student", "STUDENT" }
                });
        }
    }
}
