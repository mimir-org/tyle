using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimirorg.Authentication.Migrations
{
    public partial class SecretShouldNotBeUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MimirorgToken_Secret",
                table: "MimirorgToken");

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

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgToken_Secret",
                table: "MimirorgToken",
                column: "Secret");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MimirorgToken_Secret",
                table: "MimirorgToken");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "1e3779fe-f5d7-493e-a25f-0a0cebcfa0a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "6f8fed25-4653-4c54-80d3-e7d8673a2356");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "d70af7e6-beca-4779-b8c6-29dc1f33f8e0");

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgToken_Secret",
                table: "MimirorgToken",
                column: "Secret",
                unique: true);
        }
    }
}
