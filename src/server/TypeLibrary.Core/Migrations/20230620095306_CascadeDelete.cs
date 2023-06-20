using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                table: "AspectObject_Terminal");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                table: "AspectObject_Terminal");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Unit_Attribute_AttributeId",
                table: "Attribute_Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Unit_Unit_UnitId",
                table: "Attribute_Unit");

            migrationBuilder.AlterColumn<string>(
                name: "RdsId",
                table: "AspectObject",
                type: "nvarchar(63)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                table: "AspectObject_Terminal",
                column: "AspectObjectId",
                principalTable: "AspectObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                table: "AspectObject_Terminal",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Unit_Attribute_AttributeId",
                table: "Attribute_Unit",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Unit_Unit_UnitId",
                table: "Attribute_Unit",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                table: "AspectObject_Terminal");

            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                table: "AspectObject_Terminal");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Unit_Attribute_AttributeId",
                table: "Attribute_Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Unit_Unit_UnitId",
                table: "Attribute_Unit");

            migrationBuilder.AlterColumn<string>(
                name: "RdsId",
                table: "AspectObject",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Rds_RdsId",
                table: "AspectObject",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Terminal_AspectObject_AspectObjectId",
                table: "AspectObject_Terminal",
                column: "AspectObjectId",
                principalTable: "AspectObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_Terminal_Terminal_TerminalId",
                table: "AspectObject_Terminal",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Unit_Attribute_AttributeId",
                table: "Attribute_Unit",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Unit_Unit_UnitId",
                table: "Attribute_Unit",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }
    }
}
