using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tyle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purposes",
                table: "Purposes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Predicates",
                table: "Predicates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classifiers",
                table: "Classifiers");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "Purposes",
                newName: "Purpose");

            migrationBuilder.RenameTable(
                name: "Predicates",
                newName: "Predicate");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Medium");

            migrationBuilder.RenameTable(
                name: "Classifiers",
                newName: "Classifier");

            migrationBuilder.RenameIndex(
                name: "IX_Units_Iri",
                table: "Unit",
                newName: "IX_Unit_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Purposes_Iri",
                table: "Purpose",
                newName: "IX_Purpose_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Predicates_Iri",
                table: "Predicate",
                newName: "IX_Predicate_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Media_Iri",
                table: "Medium",
                newName: "IX_Medium_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Classifiers_Iri",
                table: "Classifier",
                newName: "IX_Classifier_Iri");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purpose",
                table: "Purpose",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Predicate",
                table: "Predicate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medium",
                table: "Medium",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classifier",
                table: "Classifier",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PredicateId = table.Column<int>(type: "int", nullable: true),
                    UnitMinCount = table.Column<int>(type: "int", nullable: false),
                    UnitMaxCount = table.Column<int>(type: "int", nullable: false),
                    ProvenanceQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangeQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularityQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Predicate_PredicateId",
                        column: x => x.PredicateId,
                        principalTable: "Predicate",
                        principalColumn: "Id");
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
                    ConstraintType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                name: "ValueListEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueConstraintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueListEntry = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
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
                name: "ValueListEntry");

            migrationBuilder.DropTable(
                name: "ValueConstraint");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purpose",
                table: "Purpose");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Predicate",
                table: "Predicate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medium",
                table: "Medium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classifier",
                table: "Classifier");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Purpose",
                newName: "Purposes");

            migrationBuilder.RenameTable(
                name: "Predicate",
                newName: "Predicates");

            migrationBuilder.RenameTable(
                name: "Medium",
                newName: "Media");

            migrationBuilder.RenameTable(
                name: "Classifier",
                newName: "Classifiers");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_Iri",
                table: "Units",
                newName: "IX_Units_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Purpose_Iri",
                table: "Purposes",
                newName: "IX_Purposes_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Predicate_Iri",
                table: "Predicates",
                newName: "IX_Predicates_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Medium_Iri",
                table: "Media",
                newName: "IX_Media_Iri");

            migrationBuilder.RenameIndex(
                name: "IX_Classifier_Iri",
                table: "Classifiers",
                newName: "IX_Classifiers_Iri");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purposes",
                table: "Purposes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Predicates",
                table: "Predicates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classifiers",
                table: "Classifiers",
                column: "Id");
        }
    }
}
