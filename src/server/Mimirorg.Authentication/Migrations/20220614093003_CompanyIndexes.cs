using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimirorg.Authentication.Migrations
{
    public partial class CompanyIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Secret",
                table: "MimirorgCompany",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "MimirorgCompany",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgCompany_Domain",
                table: "MimirorgCompany",
                column: "Domain",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgCompany_Domain_Secret",
                table: "MimirorgCompany",
                columns: new[] { "Domain", "Secret" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MimirorgCompany_Domain",
                table: "MimirorgCompany");

            migrationBuilder.DropIndex(
                name: "IX_MimirorgCompany_Domain_Secret",
                table: "MimirorgCompany");

            migrationBuilder.AlterColumn<string>(
                name: "Secret",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "b5d4522b-ebbd-446a-a339-30b621b0aa31");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "2861358b-6c7d-4b7d-9938-5679c9060b57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "2883e763-10cb-4823-96a2-a1651d242bfb");
        }
    }
}
