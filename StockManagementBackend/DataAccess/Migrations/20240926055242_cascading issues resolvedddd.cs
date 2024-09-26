using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadingissuesresolvedddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c524a46-f757-48cd-8ccc-90cb80049e94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52b22d4c-e200-431c-a193-d97ad85d6ac1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "987f7f22-3bfb-4907-bade-2863eaa7b087");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "0c524a46-f757-48cd-8ccc-90cb80049e94", null, "user", "USER" },
                    { "52b22d4c-e200-431c-a193-d97ad85d6ac1", null, "manager", "MANAGER" },
                    { "987f7f22-3bfb-4907-bade-2863eaa7b087", null, "admin", "ADMIN" }
                });
        }
    }
}
