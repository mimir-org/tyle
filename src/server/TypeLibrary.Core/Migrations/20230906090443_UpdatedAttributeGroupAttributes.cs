using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAttributeGroupAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_AttributeGroup_AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeGroupAttributes_Attribute_AttributeId",
                table: "AttributeGroupAttributes");

            migrationBuilder.DropIndex(
                name: "IX_AttributeGroupAttributes_AttributeId",
                table: "AttributeGroupAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeId",
                table: "AttributeGroupAttributes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttributeGroupAttributesLibDmId",
                table: "Attribute",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AttributeGroupAttributesLibDmId",
                table: "Attribute",
                column: "AttributeGroupAttributesLibDmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_AttributeGroupAttributes_AttributeGroupAttributesLibDmId",
                table: "Attribute",
                column: "AttributeGroupAttributesLibDmId",
                principalTable: "AttributeGroupAttributes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_AttributeGroupAttributes_AttributeGroupAttributesLibDmId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AttributeGroupAttributesLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeGroupAttributesLibDmId",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeId",
                table: "AttributeGroupAttributes",
                type: "nvarchar(63)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeGroupLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupAttributes_AttributeId",
                table: "AttributeGroupAttributes",
                column: "AttributeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeGroupAttributes_Attribute_AttributeId",
                table: "AttributeGroupAttributes",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id");
        }
    }
}
