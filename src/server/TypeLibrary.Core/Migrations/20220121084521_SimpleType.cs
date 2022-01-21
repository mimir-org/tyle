using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class SimpleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SemanticReference",
                table: "SimpleType",
                newName: "Iri");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SimpleType",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SimpleType");

            migrationBuilder.RenameColumn(
                name: "Iri",
                table: "SimpleType",
                newName: "SemanticReference");
        }
    }
}
