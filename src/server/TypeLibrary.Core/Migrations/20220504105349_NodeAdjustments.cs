using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class NodeAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_AttributeAspect_AttributeAspectId",
                table: "Node");

            migrationBuilder.DropForeignKey(
                name: "FK_Node_Blob_BlobId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Node_AttributeAspectId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Node_BlobId",
                table: "Node");

            migrationBuilder.RenameColumn(
                name: "RdsId",
                table: "Node",
                newName: "RdsCode");

            migrationBuilder.RenameColumn(
                name: "PurposeId",
                table: "Node",
                newName: "PurposeDiscipline");

            migrationBuilder.RenameColumn(
                name: "BlobId",
                table: "Node",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "AttributeAspectId",
                table: "Node",
                newName: "AttributeAspectIri");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Node",
                newName: "BlobId");

            migrationBuilder.RenameColumn(
                name: "RdsCode",
                table: "Node",
                newName: "RdsId");

            migrationBuilder.RenameColumn(
                name: "PurposeDiscipline",
                table: "Node",
                newName: "PurposeId");

            migrationBuilder.RenameColumn(
                name: "AttributeAspectIri",
                table: "Node",
                newName: "AttributeAspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_AttributeAspectId",
                table: "Node",
                column: "AttributeAspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_BlobId",
                table: "Node",
                column: "BlobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_AttributeAspect_AttributeAspectId",
                table: "Node",
                column: "AttributeAspectId",
                principalTable: "AttributeAspect",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Blob_BlobId",
                table: "Node",
                column: "BlobId",
                principalTable: "Blob",
                principalColumn: "Id");
        }
    }
}
