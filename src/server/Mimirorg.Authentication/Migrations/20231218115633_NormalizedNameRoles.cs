using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mimirorg.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedNameRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "181ba3bc-a74c-4d06-ba29-0e80d95cc20c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d22e2a4-85b5-4ba5-b596-2be880a27be8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4446917-bbbe-41e1-b156-a7b6ff819ab6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e46697ea-7cad-4da1-b399-06cf0621b475");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58e158cf-08b6-477c-82a8-a4d44a854be1", null, "Contributor", "CONTRIBUTOR" },
                    { "76392a20-acbe-491d-bb52-a67cf58dd108", null, "Administrator", "ADMINISTRATOR" },
                    { "c62d0f40-27c3-455b-8b12-56b272a87386", null, "Reader", "READER" },
                    { "ceaf8cc1-5e35-4493-8341-1838b46e6f6b", null, "Reviewer", "REVIEWER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58e158cf-08b6-477c-82a8-a4d44a854be1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76392a20-acbe-491d-bb52-a67cf58dd108");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c62d0f40-27c3-455b-8b12-56b272a87386");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ceaf8cc1-5e35-4493-8341-1838b46e6f6b");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "181ba3bc-a74c-4d06-ba29-0e80d95cc20c", null, "Administrator", null },
                    { "8d22e2a4-85b5-4ba5-b596-2be880a27be8", null, "Reviewer", null },
                    { "e4446917-bbbe-41e1-b156-a7b6ff819ab6", null, "Reader", null },
                    { "e46697ea-7cad-4da1-b399-06cf0621b475", null, "Contributor", null }
                });
        }
    }
}