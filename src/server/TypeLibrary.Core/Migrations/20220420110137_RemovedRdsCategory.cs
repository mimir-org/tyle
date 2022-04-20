using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class RemovedRdsCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interface_Rds_RdsId",
                table: "Interface");

            migrationBuilder.DropForeignKey(
                name: "FK_Node_Rds_RdsId",
                table: "Node");

            migrationBuilder.DropForeignKey(
                name: "FK_Rds_RdsCategory_RdsCategoryId",
                table: "Rds");

            migrationBuilder.DropForeignKey(
                name: "FK_Transport_Rds_RdsId",
                table: "Transport");

            migrationBuilder.DropTable(
                name: "RdsCategory");

            migrationBuilder.DropIndex(
                name: "IX_Transport_RdsId",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Rds_RdsCategoryId",
                table: "Rds");

            migrationBuilder.DropIndex(
                name: "IX_Node_RdsId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Interface_RdsId",
                table: "Interface");

            migrationBuilder.DropColumn(
                name: "Aspect",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "RdsCategoryId",
                table: "Rds");

            migrationBuilder.AddColumn<string>(
                name: "RdsName",
                table: "Transport",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "Iri",
                table: "Rds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Rds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AddColumn<string>(
                name: "RdsName",
                table: "Node",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RdsName",
                table: "Interface",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RdsName",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "RdsName",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "RdsName",
                table: "Interface");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rds",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Iri",
                table: "Rds",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Rds",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Aspect",
                table: "Rds",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Rds",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RdsCategoryId",
                table: "Rds",
                type: "nvarchar(127)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RdsCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RdsCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transport_RdsId",
                table: "Transport",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rds_RdsCategoryId",
                table: "Rds",
                column: "RdsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_RdsId",
                table: "Node",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_RdsId",
                table: "Interface",
                column: "RdsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interface_Rds_RdsId",
                table: "Interface",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Rds_RdsId",
                table: "Node",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rds_RdsCategory_RdsCategoryId",
                table: "Rds",
                column: "RdsCategoryId",
                principalTable: "RdsCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transport_Rds_RdsId",
                table: "Transport",
                column: "RdsId",
                principalTable: "Rds",
                principalColumn: "Id");
        }
    }
}
