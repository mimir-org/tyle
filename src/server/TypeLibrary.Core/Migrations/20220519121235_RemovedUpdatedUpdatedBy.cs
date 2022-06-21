using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemovedUpdatedUpdatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Symbol");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Simple");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributeSource");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributeSource");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributeQualifier");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributeQualifier");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributePredefined");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributeFormat");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributeFormat");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributeCondition");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributeCondition");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AttributeAspect");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AttributeAspect");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Attribute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Unit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Unit",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Transport",
                type: "datetime2",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Transport",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Terminal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Terminal",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Symbol",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Symbol",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Simple",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Simple",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Rds",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Rds",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Purpose",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Purpose",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Node",
                type: "datetime2",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Node",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Interface",
                type: "datetime2",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Interface",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributeSource",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributeSource",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributeQualifier",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributeQualifier",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributePredefined",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributePredefined",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributeFormat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributeFormat",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributeCondition",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributeCondition",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AttributeAspect",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AttributeAspect",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Attribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Attribute",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: true);
        }
    }
}