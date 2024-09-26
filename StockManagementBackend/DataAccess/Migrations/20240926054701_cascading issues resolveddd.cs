using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadingissuesresolveddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02c62407-7a22-41b3-8f71-1a848ccc05a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b86140f2-0269-4df3-b193-ef62dda44307");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7ba3216-a3e5-4d8c-a250-aed61c3bd24a");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "02c62407-7a22-41b3-8f71-1a848ccc05a1", null, "manager", "MANAGER" },
                    { "b86140f2-0269-4df3-b193-ef62dda44307", null, "user", "USER" },
                    { "e7ba3216-a3e5-4d8c-a250-aed61c3bd24a", null, "admin", "ADMIN" }
                });
        }
    }
}
