using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class MoreUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockClassifierMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockClassifierMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockClassifierMapping_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockClassifierMapping_Classifier_ClassifierId",
                        column: x => x.ClassifierId,
                        principalTable: "Classifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockClassifierMapping_BlockId",
                table: "BlockClassifierMapping",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockClassifierMapping_ClassifierId",
                table: "BlockClassifierMapping",
                column: "ClassifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockClassifierMapping");
        }
    }
}
