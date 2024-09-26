using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadingissuesresolveddddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37abda6f-a404-4f76-abbf-4594a588c2c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ece183f-457b-434c-a54b-dbf14cf00f24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73ced189-0ab3-49db-8aaf-3b8ad0ed0f64");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04a8b0eb-152d-4085-b4ea-b9433b82ba5f", null, "manager", "MANAGER" },
                    { "63f3c1c1-05c3-487e-a089-b178e2beee6d", null, "user", "USER" },
                    { "6b0e1b57-c0c3-4a6c-b544-0c52cd4aeaac", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04a8b0eb-152d-4085-b4ea-b9433b82ba5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63f3c1c1-05c3-487e-a089-b178e2beee6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b0e1b57-c0c3-4a6c-b544-0c52cd4aeaac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37abda6f-a404-4f76-abbf-4594a588c2c0", null, "user", "USER" },
                    { "6ece183f-457b-434c-a54b-dbf14cf00f24", null, "manager", "MANAGER" },
                    { "73ced189-0ab3-49db-8aaf-3b8ad0ed0f64", null, "admin", "ADMIN" }
                });
        }
    }
}
