using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeTables : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Purpose",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Predicate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Medium",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Classifier",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                    PredicateId = table.Column<int>(type: "int", nullable: true),
                    UnitMinCount = table.Column<int>(type: "int", nullable: false),
                    UnitMaxCount = table.Column<int>(type: "int", nullable: false),
                    ProvenanceQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangeQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularityQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeQualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContributedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ConstraintType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinCount = table.Column<int>(type: "int", nullable: true),
                    MaxCount = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    EntryValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "Units",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "Purposes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "Predicates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "Media",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "Classifiers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
