using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimirorg.Authentication.Migrations
{
    public partial class Hooks_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MimirorgHook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MimirorgHook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MimirorgHook_MimirorgCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MimirorgCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "66aa8ece-9be0-4def-baa8-3ea86f199419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "58b0c5cd-c499-40cc-8e3e-43b994143e9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "4f648c4e-e9e2-4a90-8040-9bd97d745313");

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgHook_CompanyId",
                table: "MimirorgHook",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MimirorgHook");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "MimirorgCompany");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MimirorgCompany",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d",
                column: "ConcurrencyStamp",
                value: "dea6fc09-4548-4b0a-962d-59dea6ed57f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792",
                column: "ConcurrencyStamp",
                value: "93a8a99f-779c-4a72-9fc3-24f647eb3abb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c",
                column: "ConcurrencyStamp",
                value: "3367742f-3db2-4c68-8477-6cb8f9bb0d6a");
        }
    }
}