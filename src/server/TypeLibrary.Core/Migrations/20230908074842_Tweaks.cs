using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class Tweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeTypeUnitReference");

            migrationBuilder.CreateTable(
                name: "AttributeUnitMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeUnitMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeUnitMapping_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeUnitMapping_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeUnitMapping_AttributeId",
                table: "AttributeUnitMapping",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeUnitMapping_UnitId",
                table: "AttributeUnitMapping",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeUnitMapping");

            migrationBuilder.CreateTable(
                name: "AttributeTypeUnitReference",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UoMsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTypeUnitReference", x => new { x.AttributesId, x.UoMsId });
                    table.ForeignKey(
                        name: "FK_AttributeTypeUnitReference_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeTypeUnitReference_Unit_UoMsId",
                        column: x => x.UoMsId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeTypeUnitReference_UoMsId",
                table: "AttributeTypeUnitReference",
                column: "UoMsId");
        }
    }
}
