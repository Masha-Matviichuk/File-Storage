using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Migrations
{
    public partial class AdminMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "339b20d8-7fd4-4c1d-a41f-7b9730fd5a6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9ba1f7d-3b34-4c4d-8bd4-3dd00326c51f");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b01fd64-cbb8-455d-841b-cbbb43d9bb40", "e375c059-7a85-4a7e-a866-44d4e34b0838", "user", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc4be6ad-2bb2-4fb4-ad11-747dc61e152b", "dc550ab7-9695-41e1-a4bb-65dcf98acf40", "admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b01fd64-cbb8-455d-841b-cbbb43d9bb40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc4be6ad-2bb2-4fb4-ad11-747dc61e152b");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "339b20d8-7fd4-4c1d-a41f-7b9730fd5a6d", "f0e5c9e7-9f51-4a3e-b8a6-d0d46bddd547", "user", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9ba1f7d-3b34-4c4d-8bd4-3dd00326c51f", "dd42f7d9-9d28-4e85-9d02-e727a977a3eb", "admin", null });
        }
    }
}
