using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemovedInterfaceTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interface");

            migrationBuilder.DropTable(
                name: "Transport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interface",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Interface_TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    RdsCode = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    TypeReferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interface", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interface_Interface_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Interface",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Interface_Terminal_Interface_TerminalId",
                        column: x => x.Interface_TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Transport_TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    RdsCode = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    TypeReferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transport_Terminal_Transport_TerminalId",
                        column: x => x.Transport_TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transport_Transport_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Transport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interface_FirstVersionId",
                table: "Interface",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_Interface_TerminalId",
                table: "Interface",
                column: "Interface_TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_ParentId",
                table: "Interface",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_State",
                table: "Interface",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_State_Aspect",
                table: "Interface",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_Transport_FirstVersionId",
                table: "Transport",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_ParentId",
                table: "Transport",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_State",
                table: "Transport",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_State_Aspect",
                table: "Transport",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Transport_TerminalId",
                table: "Transport",
                column: "Transport_TerminalId");
        }
    }
}