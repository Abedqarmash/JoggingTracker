using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SQL.Migrations
{
    public partial class UpdateUserEntityRelationOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1782dd14-c649-404a-b498-281ce6d5591f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "36ab65e2-e584-4efb-b45c-570fc091422e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b2fd7678-24b8-48b6-88af-9c316367fc6d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1782dd14-c649-404a-b498-281ce6d5591f", "4a7411e0-2c7a-4d77-8794-0b0a2b806ac7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36ab65e2-e584-4efb-b45c-570fc091422e", "9d65d9f5-ca85-435a-826a-03b61aed2285", "UserManager", "USERMANAGER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2fd7678-24b8-48b6-88af-9c316367fc6d", "154b177e-1fe4-4e80-9aaa-d49d8600fef5", "User", "USER" });
        }
    }
}
