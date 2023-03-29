using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuantityDatumTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuantityDatum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    QuantityDatumType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityDatum", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityDatum_State",
                table: "QuantityDatum",
                column: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuantityDatum");
        }
    }
}