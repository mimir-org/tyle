using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class CorrectAlternateKeyOnRds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Rds_RdsCode_Name",
                table: "Rds");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Rds_RdsCode_CategoryId",
                table: "Rds",
                columns: new[] { "RdsCode", "CategoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Rds_RdsCode_CategoryId",
                table: "Rds");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Rds_RdsCode_Name",
                table: "Rds",
                columns: new[] { "RdsCode", "Name" });
        }
    }
}