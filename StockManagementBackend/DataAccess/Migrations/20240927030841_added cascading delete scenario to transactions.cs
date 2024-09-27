using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedcascadingdeletescenariototransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8fbe11af-871e-4ece-907e-cd49e9c1b1a1", null, "user", "USER" },
                    { "ca57ed10-0479-4238-8908-12f21a86b595", null, "manager", "MANAGER" },
                    { "cc324826-555b-4172-9da4-2bf0915a1196", null, "admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions",
                column: "BussinessEntitiesId",
                principalTable: "BussinessEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions");

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
                    { "18390647-748e-4606-b908-c22a8b0ef390", null, "admin", "ADMIN" },
                    { "5a227af1-cbb4-4586-8987-27ff3264bd4a", null, "manager", "MANAGER" },
                    { "f28802fa-baad-45d4-9964-856869951969", null, "user", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions",
                column: "BussinessEntitiesId",
                principalTable: "BussinessEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
