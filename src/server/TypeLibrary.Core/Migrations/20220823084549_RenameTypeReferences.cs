using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RenameTypeReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Unit",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Transport",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Terminal",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Symbol",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Simple",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Rds",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Purpose",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Node",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Interface",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "AttributePredefined",
                newName: "TypeReferences");

            migrationBuilder.RenameColumn(
                name: "ContentReferences",
                table: "Attribute",
                newName: "TypeReferences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Unit",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Transport",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Terminal",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Symbol",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Simple",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Rds",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Purpose",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Node",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Interface",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "AttributePredefined",
                newName: "ContentReferences");

            migrationBuilder.RenameColumn(
                name: "TypeReferences",
                table: "Attribute",
                newName: "ContentReferences");
        }
    }
}