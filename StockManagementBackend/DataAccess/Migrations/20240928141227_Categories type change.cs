using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Categoriestypechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39bcdb0a-cffb-4796-9b7f-ceac1da7f6b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a8a16f3-bf5b-49b8-97bf-46ad86373f6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea91e2eb-d494-44dc-88ba-17f34fa2b476");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BussinessEntities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a35ad2c-baeb-4343-89cc-93a68042e557", null, "admin", "ADMIN" },
                    { "6f496e29-fc30-4916-8125-ac54454f2bac", null, "manager", "MANAGER" },
                    { "7f4600d1-e20c-4965-9b5f-bc754d743e52", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a35ad2c-baeb-4343-89cc-93a68042e557");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f496e29-fc30-4916-8125-ac54454f2bac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f4600d1-e20c-4965-9b5f-bc754d743e52");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BussinessEntities",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39bcdb0a-cffb-4796-9b7f-ceac1da7f6b2", null, "admin", "ADMIN" },
                    { "9a8a16f3-bf5b-49b8-97bf-46ad86373f6d", null, "user", "USER" },
                    { "ea91e2eb-d494-44dc-88ba-17f34fa2b476", null, "manager", "MANAGER" }
                });
        }
    }
}
