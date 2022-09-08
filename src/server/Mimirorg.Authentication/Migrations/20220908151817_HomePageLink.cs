using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimirorg.Authentication.Migrations
{
    public partial class HomePageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomePage",
                table: "MimirorgCompany",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "3dc8eaa7-de0a-48d0-a5a1-54591d4e17ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "4dd38e47-926e-46d0-9a43-80b605c114da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "37a70606-05ec-4a65-b851-ec7765903c36");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePage",
                table: "MimirorgCompany");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "c04c3e36-1428-467d-b1a1-5d64b863f38f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "ab42c697-d4eb-4222-a9dc-a49760f7c758");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "5fb457f0-9d85-4d53-9224-f25d6d608723");
        }
    }
}
