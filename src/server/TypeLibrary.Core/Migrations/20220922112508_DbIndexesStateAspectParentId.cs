using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class DbIndexesStateAspectParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transport_FirstVersionId",
                table: "Transport",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_State",
                table: "Transport",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_State_Aspect",
                table: "Transport",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_FirstVersionId",
                table: "Terminal",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_State",
                table: "Terminal",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Node_FirstVersionId",
                table: "Node",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_State",
                table: "Node",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Node_State_Aspect",
                table: "Node",
                columns: new[] { "State", "Aspect" });

            migrationBuilder.CreateIndex(
                name: "IX_Interface_FirstVersionId",
                table: "Interface",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_State",
                table: "Interface",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_State_Aspect",
                table: "Interface",
                columns: new[] { "State", "Aspect" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transport_FirstVersionId",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Transport_State",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Transport_State_Aspect",
                table: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_FirstVersionId",
                table: "Terminal");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_State",
                table: "Terminal");

            migrationBuilder.DropIndex(
                name: "IX_Node_FirstVersionId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Node_State",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Node_State_Aspect",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Interface_FirstVersionId",
                table: "Interface");

            migrationBuilder.DropIndex(
                name: "IX_Interface_State",
                table: "Interface");

            migrationBuilder.DropIndex(
                name: "IX_Interface_State_Aspect",
                table: "Interface");
        }
    }
}