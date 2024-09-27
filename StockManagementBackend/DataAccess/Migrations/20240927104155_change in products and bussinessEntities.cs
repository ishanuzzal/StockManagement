using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeinproductsandbussinessEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BussinessEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "BussinessEntities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39bcdb0a-cffb-4796-9b7f-ceac1da7f6b2", null, "admin", "ADMIN" },
                    { "9a8a16f3-bf5b-49b8-97bf-46ad86373f6d", null, "user", "USER" },
                    { "ea91e2eb-d494-44dc-88ba-17f34fa2b476", null, "manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BussinessEntities_UsersId",
                table: "BussinessEntities",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_BussinessEntities_AspNetUsers_UsersId",
                table: "BussinessEntities",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BussinessEntities_AspNetUsers_UsersId",
                table: "BussinessEntities");

            migrationBuilder.DropIndex(
                name: "IX_BussinessEntities_UsersId",
                table: "BussinessEntities");

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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BussinessEntities");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "BussinessEntities");

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
    }
}
