using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "QuantityDatum");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Attribute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Unit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Terminal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Rds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "QuantityDatum",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Attribute",
                type: "int",
                nullable: true);
        }
    }
}