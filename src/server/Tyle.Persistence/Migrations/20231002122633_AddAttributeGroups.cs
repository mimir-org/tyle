using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttributeGroupId",
                table: "Terminal_Attribute",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AttributeGroupId",
                table: "Block_Attribute",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttributeGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeGroup_Attribute",
                columns: table => new
                {
                    AttributeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroup_Attribute", x => new { x.AttributeGroupId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_AttributeGroup_Attribute_AttributeGroup_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "AttributeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeGroup_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_AttributeGroupId",
                table: "Terminal_Attribute",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Attribute_AttributeGroupId",
                table: "Block_Attribute",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroup_Attribute_AttributeId",
                table: "AttributeGroup_Attribute",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Block_Attribute_AttributeGroup_AttributeGroupId",
                table: "Block_Attribute",
                column: "AttributeGroupId",
                principalTable: "AttributeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Attribute_AttributeGroup_AttributeGroupId",
                table: "Terminal_Attribute",
                column: "AttributeGroupId",
                principalTable: "AttributeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Block_Attribute_AttributeGroup_AttributeGroupId",
                table: "Block_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Attribute_AttributeGroup_AttributeGroupId",
                table: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "AttributeGroup_Attribute");

            migrationBuilder.DropTable(
                name: "AttributeGroup");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_Attribute_AttributeGroupId",
                table: "Terminal_Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Block_Attribute_AttributeGroupId",
                table: "Block_Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeGroupId",
                table: "Terminal_Attribute");

            migrationBuilder.DropColumn(
                name: "AttributeGroupId",
                table: "Block_Attribute");
        }
    }
}