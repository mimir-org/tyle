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
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Predicate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UoMs = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ProvenanceQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RangeQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegularityQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ScopeQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Classifiers = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
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
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Classifiers = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Qualifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Value_Constraint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstraintType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllowedValues = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: true),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(38,19)", precision: 38, scale: 19, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(38,19)", precision: 38, scale: 19, nullable: true),
                    MinInclusive = table.Column<bool>(type: "bit", nullable: true),
                    MaxInclusive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Value_Constraint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Value_Constraint_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Group_Mapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Group_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Group_Mapping_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Group_Mapping_Attribute_Group_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "Attribute_Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block_Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Terminal_Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Group_Mapping_AttributeGroupId",
                table: "Attribute_Group_Mapping",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Group_Mapping_AttributeId",
                table: "Attribute_Group_Mapping",
                column: "AttributeId");

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
                name: "IX_Terminal_Attribute_AttributeId",
                table: "Terminal_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Value_Constraint_AttributeId",
                table: "Value_Constraint",
                column: "AttributeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Group_Mapping");

            migrationBuilder.DropTable(
                name: "Block_Attribute");

            migrationBuilder.DropTable(
                name: "Block_Terminal");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "Value_Constraint");

            migrationBuilder.DropTable(
                name: "Attribute_Group");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "Attribute");
        }
    }
}
