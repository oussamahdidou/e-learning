using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "videos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "syntheses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "schemas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "paragraphes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03875c8f-8e50-4312-9cf0-3913b34e70a5", null, "Admin", "ADMIN" },
                    { "53efcfea-bde0-44ba-b8c0-f50730165597", null, "Teacher", "TEACHER" },
                    { "88a1000c-3687-4904-851e-0f1bbd7efd8a", null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_videos_TeacherId",
                table: "videos",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_syntheses_TeacherId",
                table: "syntheses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_schemas_TeacherId",
                table: "schemas",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_paragraphes_TeacherId",
                table: "paragraphes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_paragraphes_AspNetUsers_TeacherId",
                table: "paragraphes",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_schemas_AspNetUsers_TeacherId",
                table: "schemas",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_syntheses_AspNetUsers_TeacherId",
                table: "syntheses",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_AspNetUsers_TeacherId",
                table: "videos",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paragraphes_AspNetUsers_TeacherId",
                table: "paragraphes");

            migrationBuilder.DropForeignKey(
                name: "FK_schemas_AspNetUsers_TeacherId",
                table: "schemas");

            migrationBuilder.DropForeignKey(
                name: "FK_syntheses_AspNetUsers_TeacherId",
                table: "syntheses");

            migrationBuilder.DropForeignKey(
                name: "FK_videos_AspNetUsers_TeacherId",
                table: "videos");

            migrationBuilder.DropIndex(
                name: "IX_videos_TeacherId",
                table: "videos");

            migrationBuilder.DropIndex(
                name: "IX_syntheses_TeacherId",
                table: "syntheses");

            migrationBuilder.DropIndex(
                name: "IX_schemas_TeacherId",
                table: "schemas");

            migrationBuilder.DropIndex(
                name: "IX_paragraphes_TeacherId",
                table: "paragraphes");

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

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "syntheses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "schemas");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "paragraphes");

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
    }
}
