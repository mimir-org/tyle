using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class ReafctorAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Attribute");

            migrationBuilder.RenameColumn(
                name: "AttributeSource",
                table: "Attribute",
                newName: "QuantityDatumSpecifiedScope");

            migrationBuilder.RenameColumn(
                name: "AttributeQualifier",
                table: "Attribute",
                newName: "QuantityDatumSpecifiedProvenance");

            migrationBuilder.RenameColumn(
                name: "AttributeFormat",
                table: "Attribute",
                newName: "QuantityDatumRegularitySpecified");

            migrationBuilder.RenameColumn(
                name: "AttributeCondition",
                table: "Attribute",
                newName: "QuantityDatumRangeSpecifying");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityDatumSpecifiedScope",
                table: "Attribute",
                newName: "AttributeSource");

            migrationBuilder.RenameColumn(
                name: "QuantityDatumSpecifiedProvenance",
                table: "Attribute",
                newName: "AttributeQualifier");

            migrationBuilder.RenameColumn(
                name: "QuantityDatumRegularitySpecified",
                table: "Attribute",
                newName: "AttributeFormat");

            migrationBuilder.RenameColumn(
                name: "QuantityDatumRangeSpecifying",
                table: "Attribute",
                newName: "AttributeCondition");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}