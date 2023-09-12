using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class RenameToBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspectObject_Attribute");

            migrationBuilder.DropTable(
                name: "AspectObject_Terminal");

            migrationBuilder.DropTable(
                name: "AspectObject");

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    SelectedAttributePredefined = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Rds_RdsId",
                        column: x => x.RdsId,
                        principalTable: "Rds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Block_Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Block_Attribute_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block_Terminal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaxQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 2147483647),
                    ConnectorDirection = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    BlockId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Terminal_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Block_Terminal_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Block_FirstVersionId",
                table: "Block",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_RdsId",
                table: "Block",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_State",
                table: "Block",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Block_State_Aspect",
                table: "Block",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_Block_Attribute_AttributeId",
                table: "Block_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Attribute_BlockId",
                table: "Block_Attribute",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Terminal_BlockId",
                table: "Block_Terminal",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Terminal_TerminalId",
                table: "Block_Terminal",
                column: "TerminalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Block_Attribute");

            migrationBuilder.DropTable(
                name: "Block_Terminal");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.CreateTable(
                name: "AspectObject",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    FirstVersionId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    SelectedAttributePredefined = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspectObject_Rds_RdsId",
                        column: x => x.RdsId,
                        principalTable: "Rds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspectObject_Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspectObjectId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectObject_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspectObject_Attribute_AspectObject_AspectObjectId",
                        column: x => x.AspectObjectId,
                        principalTable: "AspectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspectObject_Terminal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    AspectObjectId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    ConnectorDirection = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    MaxQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 2147483647),
                    MinQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectObject_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                        column: x => x.AspectObjectId,
                        principalTable: "AspectObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_FirstVersionId",
                table: "AspectObject",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_RdsId",
                table: "AspectObject",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_State",
                table: "AspectObject",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_State_Aspect",
                table: "AspectObject",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_Attribute_AspectObjectId",
                table: "AspectObject_Attribute",
                column: "AspectObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_Attribute_AttributeId",
                table: "AspectObject_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_Terminal_AspectObjectId",
                table: "AspectObject_Terminal",
                column: "AspectObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_Terminal_TerminalId",
                table: "AspectObject_Terminal",
                column: "TerminalId");
        }
    }
}