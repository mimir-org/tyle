using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSymbols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Block");

            migrationBuilder.AddColumn<int>(
                name: "SymbolId",
                table: "Block",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SvgString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymbolId = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConnectorDirection = table.Column<int>(type: "int", nullable: false),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionPoint_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Block_SymbolId",
                table: "Block",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionPoint_SymbolId",
                table: "ConnectionPoint",
                column: "SymbolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Block_Symbol_SymbolId",
                table: "Block",
                column: "SymbolId",
                principalTable: "Symbol",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Block_Symbol_SymbolId",
                table: "Block");

            migrationBuilder.DropTable(
                name: "ConnectionPoint");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropIndex(
                name: "IX_Block_SymbolId",
                table: "Block");

            migrationBuilder.DropColumn(
                name: "SymbolId",
                table: "Block");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Block",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
