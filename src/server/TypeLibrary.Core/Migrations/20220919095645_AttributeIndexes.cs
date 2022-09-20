using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class AttributeIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Attribute_FirstVersionId",
                table: "Attribute",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_State",
                table: "Attribute",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_State_Aspect",
                table: "Attribute",
                columns: new[] { "State", "Aspect" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attribute_FirstVersionId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_State",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_State_Aspect",
                table: "Attribute");
        }
    }
}