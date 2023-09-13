using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class TerminalClassifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockTypeClassifierReference");

            migrationBuilder.DropTable(
                name: "ClassifierReferenceTerminalType");

            migrationBuilder.CreateTable(
                name: "TerminalClassifierMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalClassifierMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalClassifierMapping_Classifier_ClassifierId",
                        column: x => x.ClassifierId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminalClassifierMapping_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalClassifierMapping_ClassifierId",
                table: "TerminalClassifierMapping",
                column: "ClassifierId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalClassifierMapping_TerminalId",
                table: "TerminalClassifierMapping",
                column: "TerminalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TerminalClassifierMapping");

            migrationBuilder.CreateTable(
                name: "BlockTypeClassifierReference",
                columns: table => new
                {
                    BlocksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifiersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTypeClassifierReference", x => new { x.BlocksId, x.ClassifiersId });
                    table.ForeignKey(
                        name: "FK_BlockTypeClassifierReference_Block_BlocksId",
                        column: x => x.BlocksId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockTypeClassifierReference_Classifier_ClassifiersId",
                        column: x => x.ClassifiersId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassifierReferenceTerminalType",
                columns: table => new
                {
                    ClassifiersId = table.Column<int>(type: "int", nullable: false),
                    TerminalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifierReferenceTerminalType", x => new { x.ClassifiersId, x.TerminalsId });
                    table.ForeignKey(
                        name: "FK_ClassifierReferenceTerminalType_Classifier_ClassifiersId",
                        column: x => x.ClassifiersId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassifierReferenceTerminalType_Terminal_TerminalsId",
                        column: x => x.TerminalsId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockTypeClassifierReference_ClassifiersId",
                table: "BlockTypeClassifierReference",
                column: "ClassifiersId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifierReferenceTerminalType_TerminalsId",
                table: "ClassifierReferenceTerminalType",
                column: "TerminalsId");
        }
    }
}
