using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mimirorg.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class OnlyUseRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MimirorgHook");

            migrationBuilder.DropTable(
                name: "MimirorgCompany");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d465b3-90cf-4408-a685-14ff462e549d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cabdda90-6e90-4b92-b309-4f5b3784c792");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MimirorgCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HomePage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MimirorgCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MimirorgCompany_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MimirorgHook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b5d465b3-90cf-4408-a685-14ff462e549d", null, "Administrator", "ADMINISTRATOR" },
                    { "cabdda90-6e90-4b92-b309-4f5b3784c792", null, "Account Manager", "ACCOUNTMANAGER" },
                    { "f6d7df3a-bc9f-4a79-a2a0-c001a83c2d6c", null, "Moderator", "MODERATOR" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgCompany_ManagerId",
                table: "MimirorgCompany",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgCompany_Name",
                table: "MimirorgCompany",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MimirorgHook_CompanyId",
                table: "MimirorgHook",
                column: "CompanyId");
        }
    }
}