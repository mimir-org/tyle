using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class TypeLibrary_Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iri",
                table: "Collection",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Unit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Unit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Source",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Source",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Source",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Source",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RdsCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RdsCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "RdsCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "RdsCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Qualifier",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Qualifier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Qualifier",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Qualifier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Purpose",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Purpose",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Purpose",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Purpose",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Location",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Location",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Format",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Format",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Format",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Format",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Condition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Condition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Condition",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Condition",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Collection",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Collection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Collection",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Collection",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LibraryType_Collection",
                columns: table => new
                {
                    CollectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryType_Collection", x => new { x.CollectionId, x.LibraryTypeId });
                    table.ForeignKey(
                        name: "FK_LibraryType_Collection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryType_Collection_LibraryType_LibraryTypeId",
                        column: x => x.LibraryTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_Collection_LibraryTypeId",
                table: "LibraryType_Collection",
                column: "LibraryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryType_Collection");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Source");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Source");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Source");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Source");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RdsCategory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RdsCategory");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "RdsCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RdsCategory");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Qualifier");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Qualifier");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Qualifier");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Qualifier");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Collection");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Collection",
                newName: "Iri");
        }
    }
}
