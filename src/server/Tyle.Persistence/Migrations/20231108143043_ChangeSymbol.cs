using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSymbol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SvgString",
                table: "Symbol",
                newName: "Path");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Symbol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Symbol",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Symbol");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Symbol",
                newName: "SvgString");
        }
    }
}