using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class TerminalsConnectionPointsAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionPointId",
                table: "Block_Terminal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Block_Terminal_ConnectionPointId",
                table: "Block_Terminal",
                column: "ConnectionPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Block_Terminal_ConnectionPoint_ConnectionPointId",
                table: "Block_Terminal",
                column: "ConnectionPointId",
                principalTable: "ConnectionPoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Block_Terminal_ConnectionPoint_ConnectionPointId",
                table: "Block_Terminal");

            migrationBuilder.DropIndex(
                name: "IX_Block_Terminal_ConnectionPointId",
                table: "Block_Terminal");

            migrationBuilder.DropColumn(
                name: "ConnectionPointId",
                table: "Block_Terminal");
        }
    }
}