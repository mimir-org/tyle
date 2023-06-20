using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class MoreCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Attribute_AspectObject_AspectObjectId",
                table: "AspectObject_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                table: "AspectObject_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Attribute_Attribute_AttributeId",
                table: "Terminal_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Attribute_Terminal_TerminalId",
                table: "Terminal_Attribute");

            migrationBuilder.AddColumn<string>(
                name: "AspectObjectLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerminalLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AspectObjectLibDmId",
                table: "Attribute",
                column: "AspectObjectLibDmId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_TerminalLibDmId",
                table: "Attribute",
                column: "TerminalLibDmId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Attribute_AspectObject_AspectObjectId",
                table: "AspectObject_Attribute",
                column: "AspectObjectId",
                principalTable: "AspectObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                table: "AspectObject_Attribute",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_AspectObject_AspectObjectLibDmId",
                table: "Attribute",
                column: "AspectObjectLibDmId",
                principalTable: "AspectObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Terminal_TerminalLibDmId",
                table: "Attribute",
                column: "TerminalLibDmId",
                principalTable: "Terminal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Attribute_Attribute_AttributeId",
                table: "Terminal_Attribute",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Attribute_Terminal_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Attribute_AspectObject_AspectObjectId",
                table: "AspectObject_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                table: "AspectObject_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_AspectObject_AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Terminal_TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Attribute_Attribute_AttributeId",
                table: "Terminal_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Attribute_Terminal_TerminalId",
                table: "Terminal_Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Attribute_AspectObject_AspectObjectId",
                table: "AspectObject_Attribute",
                column: "AspectObjectId",
                principalTable: "AspectObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Attribute_Attribute_AttributeId",
                table: "AspectObject_Attribute",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Attribute_Attribute_AttributeId",
                table: "Terminal_Attribute",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Attribute_Terminal_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id");
        }
    }
}