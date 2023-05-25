using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class MatchingConfigOnAllTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Unit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Terminal",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Terminal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Rds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "QuantityDatum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Attribute",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspectObject",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Unit",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Terminal",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Terminal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Rds",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "QuantityDatum",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Attribute",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspectObject",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);
        }
    }
}