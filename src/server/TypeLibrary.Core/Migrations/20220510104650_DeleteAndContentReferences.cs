using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class DeleteAndContentReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Transport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Transport",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Terminal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Terminal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Symbol",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Symbol",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Simple",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Simple",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Rds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Rds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Iri",
                table: "Purpose",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Purpose",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Purpose",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Node",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Node",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Interface",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Interface",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributeSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributeSource",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributeQualifier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributeQualifier",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributePredefined",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributePredefined",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributeFormat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributeFormat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributeCondition",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributeCondition",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "AttributeAspect",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AttributeAspect",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContentReferences",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Attribute",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributeSource");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributeSource");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributeQualifier");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributeQualifier");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributeFormat");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributeFormat");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributeCondition");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributeCondition");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "AttributeAspect");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AttributeAspect");

            migrationBuilder.DropColumn(
                name: "ContentReferences",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "Iri",
                table: "Purpose",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
