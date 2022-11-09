using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class TerminalMinMaxQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Node_Terminal",
                newName: "MinQuantity");

            migrationBuilder.AddColumn<int>(
                name: "MaxQuantity",
                table: "Node_Terminal",
                type: "int",
                nullable: false,
                defaultValue: 2147483647);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxQuantity",
                table: "Node_Terminal");

            migrationBuilder.RenameColumn(
                name: "MinQuantity",
                table: "Node_Terminal",
                newName: "Quantity");
        }
    }
}