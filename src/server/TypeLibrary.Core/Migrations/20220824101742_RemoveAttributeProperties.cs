using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemoveAttributeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Attribute_ParentId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_ParentId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeType",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Attribute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributeType",
                table: "Attribute",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Attribute",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ParentId",
                table: "Attribute",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Attribute_ParentId",
                table: "Attribute",
                column: "ParentId",
                principalTable: "Attribute",
                principalColumn: "Id");
        }
    }
}
