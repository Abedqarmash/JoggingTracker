using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SQL.Migrations
{
    public partial class AddLocationToJoggingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5edbdc96-e0b8-4584-83dd-afda003a2a70");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8040e987-3b28-40a0-8be1-429099b5ba64");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "85d01904-98e5-4161-930c-6df5a3f0a4df");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JoggingEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JoggingEntities");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5edbdc96-e0b8-4584-83dd-afda003a2a70", "3e07ea10-6682-4162-91d5-6345c47447a4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8040e987-3b28-40a0-8be1-429099b5ba64", "ec4d3729-88d0-45b1-b43e-f7d40f7de534", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85d01904-98e5-4161-930c-6df5a3f0a4df", "a425336f-433f-4e28-b953-197ac8bd6696", "UserManager", "USERMANAGER" });
        }
    }
}
