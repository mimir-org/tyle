using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedRdsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RdsCode",
                table: "AspectObject");

            migrationBuilder.DropColumn(
                name: "RdsName",
                table: "AspectObject");

            migrationBuilder.AddColumn<string>(
                name: "RdsId",
                table: "AspectObject",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(63)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rds", x => x.Id);
                    table.UniqueConstraint("AK_Rds_RdsCode_Name", x => new { x.RdsCode, x.Name });
                    table.ForeignKey(
                        name: "FK_Rds_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_RdsId",
                table: "AspectObject",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rds_CategoryId",
                table: "Rds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rds_State",
                table: "Rds",
                column: "State");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject");

            migrationBuilder.DropTable(
                name: "Rds");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_AspectObject_RdsId",
                table: "AspectObject");

            migrationBuilder.DropColumn(
                name: "RdsId",
                table: "AspectObject");

            migrationBuilder.AddColumn<string>(
                name: "RdsCode",
                table: "AspectObject",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RdsName",
                table: "AspectObject",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }
    }
}