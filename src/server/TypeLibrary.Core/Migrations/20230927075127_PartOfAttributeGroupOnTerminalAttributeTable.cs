using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class PartOfAttributeGroupOnTerminalAttributeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartOfAttributeGroup",
                table: "Terminal_Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartOfAttributeGroup",
                table: "Block_Attribute",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartOfAttributeGroup",
                table: "Terminal_Attribute");

            migrationBuilder.DropColumn(
                name: "PartOfAttributeGroup",
                table: "Block_Attribute");
        }
    }
}
