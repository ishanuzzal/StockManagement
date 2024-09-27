using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedkeysinbussinessentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fbe11af-871e-4ece-907e-cd49e9c1b1a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca57ed10-0479-4238-8908-12f21a86b595");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc324826-555b-4172-9da4-2bf0915a1196");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01ee4608-f2a5-4c0d-9895-0416041606b3", null, "admin", "ADMIN" },
                    { "d53253fe-d129-406e-844e-35bcfb7dc3c7", null, "manager", "MANAGER" },
                    { "e397af88-55ac-4446-b546-958a79ad5040", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01ee4608-f2a5-4c0d-9895-0416041606b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d53253fe-d129-406e-844e-35bcfb7dc3c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e397af88-55ac-4446-b546-958a79ad5040");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8fbe11af-871e-4ece-907e-cd49e9c1b1a1", null, "user", "USER" },
                    { "ca57ed10-0479-4238-8908-12f21a86b595", null, "manager", "MANAGER" },
                    { "cc324826-555b-4172-9da4-2bf0915a1196", null, "admin", "ADMIN" }
                });
        }
    }
}
