using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class TehnikaInspectionDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_Tehnikas_TehnikaId",
                table: "Inspections");

            migrationBuilder.DropIndex(
                name: "IX_Inspections_TehnikaId",
                table: "Inspections");

            migrationBuilder.DropColumn(
                name: "TehnikaId",
                table: "Inspections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TehnikaId",
                table: "Inspections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_TehnikaId",
                table: "Inspections",
                column: "TehnikaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_Tehnikas_TehnikaId",
                table: "Inspections",
                column: "TehnikaId",
                principalTable: "Tehnikas",
                principalColumn: "TehnikaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
