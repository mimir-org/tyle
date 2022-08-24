using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemoveAttributeAspectIri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeAspectIri",
                table: "Node");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributeAspectIri",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);
        }
    }
}