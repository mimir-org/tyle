using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeGroupId",
                table: "AttributeGroup");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AttributeGroupAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeGroupLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AttributeGroupLibDmId",
                table: "Attribute",
                column: "AttributeGroupLibDmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_AttributeGroup_AttributeGroupLibDmId",
                table: "Attribute",
                column: "AttributeGroupLibDmId",
                principalTable: "AttributeGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_AttributeGroup_AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AttributeGroupAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "AttributeGroupId",
                table: "AttributeGroup",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
