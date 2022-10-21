using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class AttributeTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Interface");

            migrationBuilder.DropTable(
                name: "Attribute_Node");

            migrationBuilder.DropTable(
                name: "Attribute_Transport");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Transport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Terminal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Node",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Interface",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Interface");

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    Discipline = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    QuantityDatumRangeSpecifying = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    QuantityDatumRegularitySpecified = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    QuantityDatumSpecifiedProvenance = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    QuantityDatumSpecifiedScope = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Select = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    SelectValuesString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    TypeReferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Interface",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    InterfaceId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Interface", x => new { x.AttributeId, x.InterfaceId });
                    table.ForeignKey(
                        name: "FK_Attribute_Interface_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Interface_Interface_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "Interface",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Node",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Node", x => new { x.AttributeId, x.NodeId });
                    table.ForeignKey(
                        name: "FK_Attribute_Node_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Node_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Transport",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    TransportId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Transport", x => new { x.AttributeId, x.TransportId });
                    table.ForeignKey(
                        name: "FK_Attribute_Transport_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Transport_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminal_Attribute",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Attribute", x => new { x.AttributeId, x.TerminalId });
                    table.ForeignKey(
                        name: "FK_Terminal_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminal_Attribute_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Interface_InterfaceId",
                table: "Attribute_Interface",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Node_NodeId",
                table: "Attribute_Node",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Transport_TransportId",
                table: "Attribute_Transport",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId");
        }
    }
}