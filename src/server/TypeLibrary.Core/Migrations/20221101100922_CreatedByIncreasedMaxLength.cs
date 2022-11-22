using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class CreatedByIncreasedMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Transport",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Terminal",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(31)",
                oldMaxLength: 31);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Symbol",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(31)",
                oldMaxLength: 31);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Log",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Interface",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AttributePredefined",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(31)",
                oldMaxLength: 31);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Transport",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Terminal",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Symbol",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Node",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Log",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Interface",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AttributePredefined",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);
        }
    }
}