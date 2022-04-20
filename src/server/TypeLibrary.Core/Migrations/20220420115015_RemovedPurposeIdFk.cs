using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemovedPurposeIdFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interface_Purpose_PurposeId",
                table: "Interface");

            migrationBuilder.DropForeignKey(
                name: "FK_Node_Purpose_PurposeId",
                table: "Node");

            migrationBuilder.DropForeignKey(
                name: "FK_Transport_Purpose_PurposeId",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Transport_PurposeId",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Node_PurposeId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Interface_PurposeId",
                table: "Interface");

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Transport",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeName",
                table: "Transport",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeName",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Interface",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeName",
                table: "Interface",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurposeName",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "PurposeName",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "PurposeName",
                table: "Interface");

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Transport",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "PurposeId",
                table: "Interface",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.CreateIndex(
                name: "IX_Transport_PurposeId",
                table: "Transport",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_PurposeId",
                table: "Node",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_PurposeId",
                table: "Interface",
                column: "PurposeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interface_Purpose_PurposeId",
                table: "Interface",
                column: "PurposeId",
                principalTable: "Purpose",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Purpose_PurposeId",
                table: "Node",
                column: "PurposeId",
                principalTable: "Purpose",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transport_Purpose_PurposeId",
                table: "Transport",
                column: "PurposeId",
                principalTable: "Purpose",
                principalColumn: "Id");
        }
    }
}
