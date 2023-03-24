using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeReferencesToTypeReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "TypeReferences",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "Unit",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "Terminal",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "Symbol",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "Node",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "AttributePredefined",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReference",
                table: "Attribute",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "TypeReference",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "Terminal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "Symbol",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "Node",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "AttributePredefined",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReferences",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
