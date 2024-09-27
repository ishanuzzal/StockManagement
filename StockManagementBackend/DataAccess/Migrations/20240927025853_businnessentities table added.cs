using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class businnessentitiestableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0e86a5b-e3f1-473f-8778-1febb0a9455b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7cb2f53-6291-4210-a742-0694ecaeabb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6fc55da-2834-4aee-a766-16cd401ee322");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "BussinessEntitiesId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BussinessEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinessEntities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34cdb9d7-c066-424a-a7c5-655155927584", null, "admin", "ADMIN" },
                    { "69599223-f095-4d98-9280-9f535dc71c0a", null, "manager", "MANAGER" },
                    { "c5da32d0-d665-495d-b8aa-f399a98ecd93", null, "user", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BussinessEntitiesId",
                table: "Transactions",
                column: "BussinessEntitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions",
                column: "BussinessEntitiesId",
                principalTable: "BussinessEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BussinessEntities_BussinessEntitiesId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BussinessEntities");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BussinessEntitiesId",
                table: "Transactions");

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

            migrationBuilder.DropColumn(
                name: "BussinessEntitiesId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "Transactions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b0e86a5b-e3f1-473f-8778-1febb0a9455b", null, "manager", "MANAGER" },
                    { "b7cb2f53-6291-4210-a742-0694ecaeabb3", null, "user", "USER" },
                    { "f6fc55da-2834-4aee-a766-16cd401ee322", null, "admin", "ADMIN" }
                });
        }
    }
}
