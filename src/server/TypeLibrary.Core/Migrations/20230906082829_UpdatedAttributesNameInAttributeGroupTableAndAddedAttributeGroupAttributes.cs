using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAttributesNameInAttributeGroupTableAndAddedAttributeGroupAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attribute",
                table: "AttributeGroup");

            migrationBuilder.AddColumn<string>(
                name: "AttributeGroupLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttributeGroupAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeGroupId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroupAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeGroupAttributes_AttributeGroup_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "AttributeGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttributeGroupAttributes_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AttributeGroupLibDmId",
                table: "Attribute",
                column: "AttributeGroupLibDmId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupAttributes_AttributeGroupId",
                table: "AttributeGroupAttributes",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupAttributes_AttributeId",
                table: "AttributeGroupAttributes",
                column: "AttributeId");

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

            migrationBuilder.DropTable(
                name: "AttributeGroupAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeGroupLibDmId",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "Attribute",
                table: "AttributeGroup",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }
    }
}
