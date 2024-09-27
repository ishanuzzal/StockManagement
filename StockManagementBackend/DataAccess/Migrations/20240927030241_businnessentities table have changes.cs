using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class businnessentitiestablehavechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34cdb9d7-c066-424a-a7c5-655155927584");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69599223-f095-4d98-9280-9f535dc71c0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5da32d0-d665-495d-b8aa-f399a98ecd93");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "BussinessEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "BussinessEntities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18390647-748e-4606-b908-c22a8b0ef390", null, "admin", "ADMIN" },
                    { "5a227af1-cbb4-4586-8987-27ff3264bd4a", null, "manager", "MANAGER" },
                    { "f28802fa-baad-45d4-9964-856869951969", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18390647-748e-4606-b908-c22a8b0ef390");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a227af1-cbb4-4586-8987-27ff3264bd4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f28802fa-baad-45d4-9964-856869951969");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "BussinessEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "BussinessEntities");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34cdb9d7-c066-424a-a7c5-655155927584", null, "admin", "ADMIN" },
                    { "69599223-f095-4d98-9280-9f535dc71c0a", null, "manager", "MANAGER" },
                    { "c5da32d0-d665-495d-b8aa-f399a98ecd93", null, "user", "USER" }
                });
        }
    }
}
