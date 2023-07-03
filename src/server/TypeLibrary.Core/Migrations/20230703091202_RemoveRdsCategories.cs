using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRdsCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_AspectObject_AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Terminal_TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Rds_Category_CategoryId",
                table: "Rds");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Rds_RdsCode_CategoryId",
                table: "Rds");

            migrationBuilder.DropIndex(
                name: "IX_Rds_CategoryId",
                table: "Rds");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Rds");

            migrationBuilder.DropColumn(
                name: "AspectObjectLibDmId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "TerminalLibDmId",
                table: "Attribute");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Rds_RdsCode",
                table: "Rds",
                column: "RdsCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Rds_RdsCode",
                table: "Rds");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Rds",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AspectObjectLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerminalLibDmId",
                table: "Attribute",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Rds_RdsCode_CategoryId",
                table: "Rds",
                columns: new[] { "RdsCode", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rds_CategoryId",
                table: "Rds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AspectObjectLibDmId",
                table: "Attribute",
                column: "AspectObjectLibDmId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_TerminalLibDmId",
                table: "Attribute",
                column: "TerminalLibDmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_AspectObject_AspectObjectLibDmId",
                table: "Attribute",
                column: "AspectObjectLibDmId",
                principalTable: "AspectObject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Terminal_TerminalLibDmId",
                table: "Attribute",
                column: "TerminalLibDmId",
                principalTable: "Terminal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rds_Category_CategoryId",
                table: "Rds",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}