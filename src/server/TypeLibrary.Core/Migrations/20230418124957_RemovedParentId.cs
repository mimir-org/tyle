using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemovedParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspectObject_AspectObject_ParentId",
                table: "AspectObject");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Terminal_ParentId",
                table: "Terminal");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_ParentId",
                table: "Terminal");

            migrationBuilder.DropIndex(
                name: "IX_AspectObject_ParentId",
                table: "AspectObject");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AspectObject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Terminal",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "AspectObject",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_ParentId",
                table: "Terminal",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspectObject_ParentId",
                table: "AspectObject",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspectObject_AspectObject_ParentId",
                table: "AspectObject",
                column: "ParentId",
                principalTable: "AspectObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Terminal_ParentId",
                table: "Terminal",
                column: "ParentId",
                principalTable: "Terminal",
                principalColumn: "Id");
        }
    }
}
