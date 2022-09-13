using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class DeletedColumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Terminal",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Symbol",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Simple",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Rds",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Purpose",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AttributePredefined",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Attribute",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Attribute");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Transport",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Terminal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Symbol",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Simple",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Rds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Purpose",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Node",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Interface",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributePredefined",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Attribute",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
