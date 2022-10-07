using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class LogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ObjectType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    LogTypeValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    User = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogType",
                table: "Log",
                column: "LogType");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectId",
                table: "Log",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectId_ObjectType_LogType",
                table: "Log",
                columns: new[] { "ObjectId", "ObjectType", "LogType" });

            migrationBuilder.CreateIndex(
                name: "IX_Log_ObjectType",
                table: "Log",
                column: "ObjectType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}