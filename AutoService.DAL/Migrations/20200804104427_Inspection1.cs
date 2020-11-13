using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class Inspection1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_Autos_AutoId",
                table: "Inspections");

            migrationBuilder.AlterColumn<int>(
                name: "AutoId",
                table: "Inspections",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_Autos_AutoId",
                table: "Inspections",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "AutoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_Autos_AutoId",
                table: "Inspections");

            migrationBuilder.AlterColumn<int>(
                name: "AutoId",
                table: "Inspections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_Autos_AutoId",
                table: "Inspections",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "AutoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
