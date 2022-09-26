using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemovedSimpleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Simple");

            migrationBuilder.DropTable(
                name: "Simple_Node");

            migrationBuilder.DropTable(
                name: "Simple");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simple",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    TypeReferences = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simple", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Simple",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    SimpleId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Simple", x => new { x.AttributeId, x.SimpleId });
                    table.ForeignKey(
                        name: "FK_Attribute_Simple_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Simple_Simple_SimpleId",
                        column: x => x.SimpleId,
                        principalTable: "Simple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simple_Node",
                columns: table => new
                {
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    SimpleId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simple_Node", x => new { x.NodeId, x.SimpleId });
                    table.ForeignKey(
                        name: "FK_Simple_Node_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Simple_Node_Simple_SimpleId",
                        column: x => x.SimpleId,
                        principalTable: "Simple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Simple_SimpleId",
                table: "Attribute_Simple",
                column: "SimpleId");

            migrationBuilder.CreateIndex(
                name: "IX_Simple_Node_SimpleId",
                table: "Simple_Node",
                column: "SimpleId");
        }
    }
}