using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    AttributeGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributePredefined",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    IsMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    ValueStringList = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributePredefined", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectFirstVersionId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    ObjectVersion = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    ObjectType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false),
                    User = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    LogTypeValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityDatum",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    QuantityDatumType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityDatum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rds", x => x.Id);
                    table.UniqueConstraint("AK_Rds_RdsCode", x => x.RdsCode);
                });

            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terminal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeGroupAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeGroupId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroupAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeGroupAttributes_AttributeGroup_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "AttributeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeGroupAttributes_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Terminal_Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerminalId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Attribute", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Attribute_Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AttributeId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Unit_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Unit_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Attribute_State",
                table: "Attribute",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Unit_AttributeId",
                table: "Attribute_Unit",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Unit_UnitId",
                table: "Attribute_Unit",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupAttributes_AttributeGroupId",
                table: "AttributeGroupAttributes",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupAttributes_AttributeId",
                table: "AttributeGroupAttributes",
                column: "AttributeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogType",
                table: "Log",
                column: "LogType");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectFirstVersionId",
                table: "Log",
                column: "ObjectFirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectId",
                table: "Log",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectId_ObjectFirstVersionId_ObjectType_LogType",
                table: "Log",
                columns: new[] { "ObjectId", "ObjectFirstVersionId", "ObjectType", "LogType" });

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectType",
                table: "Log",
                column: "ObjectType");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityDatum_State",
                table: "QuantityDatum",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Rds_State",
                table: "Rds",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_State",
                table: "Terminal",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_AttributeId",
                table: "Terminal_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_State",
                table: "Unit",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_TypeReference",
                table: "Unit",
                column: "TypeReference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Unit");

            migrationBuilder.DropTable(
                name: "AttributeGroupAttributes");

            migrationBuilder.DropTable(
                name: "AttributePredefined");

            migrationBuilder.DropTable(
                name: "Block_Attribute");

            migrationBuilder.DropTable(
                name: "Block_Terminal");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "QuantityDatum");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "AttributeGroup");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "Rds");
        }
    }
}