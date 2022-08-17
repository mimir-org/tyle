using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RefactorAspectAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeAspect");

            migrationBuilder.AddColumn<string>(
                name: "AttributeType",
                table: "Attribute",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeType",
                table: "Attribute");

            migrationBuilder.CreateTable(
                name: "AttributeAspect",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    ContentReferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeAspect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeAspect_AttributeAspect_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AttributeAspect",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeAspect_ParentId",
                table: "AttributeAspect",
                column: "ParentId");
        }
    }
}