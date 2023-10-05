using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predicate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predicate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purpose",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purpose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PredicateId = table.Column<int>(type: "int", nullable: true),
                    UnitMinCount = table.Column<int>(type: "int", nullable: false),
                    UnitMaxCount = table.Column<int>(type: "int", nullable: false),
                    ProvenanceQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RangeQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegularityQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ScopeQualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Predicate_PredicateId",
                        column: x => x.PredicateId,
                        principalTable: "Predicate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurposeId = table.Column<int>(type: "int", nullable: true),
                    Notation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Purpose_PurposeId",
                        column: x => x.PurposeId,
                        principalTable: "Purpose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Terminal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurposeId = table.Column<int>(type: "int", nullable: true),
                    Notation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MediumId = table.Column<int>(type: "int", nullable: true),
                    Qualifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminal_Medium_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Medium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Terminal_Purpose_PurposeId",
                        column: x => x.PurposeId,
                        principalTable: "Purpose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Unit",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Unit", x => new { x.AttributeId, x.UnitId });
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
                name: "ValueConstraint",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstraintType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: true),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(38,19)", precision: 38, scale: 19, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(38,19)", precision: 38, scale: 19, nullable: true),
                    MinInclusive = table.Column<bool>(type: "bit", nullable: true),
                    MaxInclusive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueConstraint", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_ValueConstraint_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block_Attribute",
                columns: table => new
                {
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block_Attribute", x => new { x.BlockId, x.AttributeId });
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
                name: "Block_Classifier",
                columns: table => new
                {
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block_Classifier", x => new { x.BlockId, x.ClassifierId });
                    table.ForeignKey(
                        name: "FK_Block_Classifier_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Block_Classifier_Classifier_ClassifierId",
                        column: x => x.ClassifierId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block_Terminal",
                columns: table => new
                {
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block_Terminal", x => new { x.BlockId, x.TerminalId, x.Direction });
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
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Attribute", x => new { x.TerminalId, x.AttributeId });
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
                name: "Terminal_Classifier",
                columns: table => new
                {
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Classifier", x => new { x.TerminalId, x.ClassifierId });
                    table.ForeignKey(
                        name: "FK_Terminal_Classifier_Classifier_ClassifierId",
                        column: x => x.ClassifierId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminal_Classifier_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueListEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueConstraintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueListEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueListEntry_ValueConstraint_ValueConstraintId",
                        column: x => x.ValueConstraintId,
                        principalTable: "ValueConstraint",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_PredicateId",
                table: "Attribute",
                column: "PredicateId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Unit_UnitId",
                table: "Attribute_Unit",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_PurposeId",
                table: "Block",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Attribute_AttributeId",
                table: "Block_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Classifier_ClassifierId",
                table: "Block_Classifier",
                column: "ClassifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Terminal_TerminalId",
                table: "Block_Terminal",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_MediumId",
                table: "Terminal",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_PurposeId",
                table: "Terminal",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_AttributeId",
                table: "Terminal_Attribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Classifier_ClassifierId",
                table: "Terminal_Classifier",
                column: "ClassifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueListEntry_ValueConstraintId",
                table: "ValueListEntry",
                column: "ValueConstraintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Unit");

            migrationBuilder.DropTable(
                name: "Block_Attribute");

            migrationBuilder.DropTable(
                name: "Block_Classifier");

            migrationBuilder.DropTable(
                name: "Block_Terminal");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "Terminal_Classifier");

            migrationBuilder.DropTable(
                name: "ValueListEntry");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Classifier");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "ValueConstraint");

            migrationBuilder.DropTable(
                name: "Medium");

            migrationBuilder.DropTable(
                name: "Purpose");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Predicate");
        }
    }
}