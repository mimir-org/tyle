using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unit_Iri",
                table: "Unit",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_State",
                table: "Terminal",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Symbol_Iri",
                table: "Symbol",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purpose_Iri",
                table: "Purpose",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predicate_Iri",
                table: "Predicate",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medium_Iri",
                table: "Medium",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classifier_Iri",
                table: "Classifier",
                column: "Iri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Block_State",
                table: "Block",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_State",
                table: "Attribute",
                column: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unit_Iri",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_State",
                table: "Terminal");

            migrationBuilder.DropIndex(
                name: "IX_Symbol_Iri",
                table: "Symbol");

            migrationBuilder.DropIndex(
                name: "IX_Purpose_Iri",
                table: "Purpose");

            migrationBuilder.DropIndex(
                name: "IX_Predicate_Iri",
                table: "Predicate");

            migrationBuilder.DropIndex(
                name: "IX_Medium_Iri",
                table: "Medium");

            migrationBuilder.DropIndex(
                name: "IX_Classifier_Iri",
                table: "Classifier");

            migrationBuilder.DropIndex(
                name: "IX_Block_State",
                table: "Block");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_State",
                table: "Attribute");
        }
    }
}