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
                name: "AspectObject",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsCode = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    SelectedAttributePredefined = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspectObject_AspectObject_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspectObject",
                        principalColumn: "Id");
                });

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
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
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
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectVersion = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ObjectFirstVersionId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false),
                    User = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ObjectType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    LogTypeValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
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
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    QuantityDatumType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityDatum", x => x.Id);
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
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminal_Terminal_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
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
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspectObject_Attribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspectObject_Terminal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaxQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 2147483647),
                    ConnectorDirection = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    AspectObjectId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectObject_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                        column: x => x.AspectObjectId,
                        principalTable: "AspectObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Terminal_Attribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminal_Attribute_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Unit_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_FirstVersionId",
                table: "AspectObject",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_ParentId",
                table: "AspectObject",
                column: "ParentId");

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
                name: "IX_Terminal_ParentId",
                table: "Terminal",
                column: "ParentId");

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
                name: "AspectObject_Attribute");

            migrationBuilder.DropTable(
                name: "AspectObject_Terminal");

            migrationBuilder.DropTable(
                name: "Attribute_Unit");

            migrationBuilder.DropTable(
                name: "AttributePredefined");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "QuantityDatum");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "AspectObject");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Terminal");
        }
    }
}
