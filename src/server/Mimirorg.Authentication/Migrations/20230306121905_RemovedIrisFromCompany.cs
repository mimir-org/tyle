using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimirorg.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIrisFromCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iris",
                table: "MimirorgCompany");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Iris",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "38b5034a-86be-4076-95f7-591f05af5b2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "48f9b5fd-d285-4580-b809-58e4c4915817");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "f51e36af-b921-44f2-b9a9-06bced4a8005");
        }
    }
}