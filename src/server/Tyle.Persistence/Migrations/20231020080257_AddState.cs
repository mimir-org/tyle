using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxInclusive",
                table: "ValueConstraint");

            migrationBuilder.DropColumn(
                name: "MinInclusive",
                table: "ValueConstraint");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Terminal",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Block",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Attribute",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Block");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Attribute");

            migrationBuilder.AddColumn<bool>(
                name: "MaxInclusive",
                table: "ValueConstraint",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MinInclusive",
                table: "ValueConstraint",
                type: "bit",
                nullable: true);
        }
    }
}