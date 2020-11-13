using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class TehnikaDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Tehnikas_TehnikaId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Servs_Tehnikas_TehnikaId",
                table: "Servs");

            migrationBuilder.DropTable(
                name: "Tehnikas");

            migrationBuilder.DropIndex(
                name: "IX_Servs_TehnikaId",
                table: "Servs");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_TehnikaId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "TehnikaId",
                table: "Servs");

            migrationBuilder.DropColumn(
                name: "TehnikaId",
                table: "Insurances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TehnikaId",
                table: "Servs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TehnikaId",
                table: "Insurances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tehnikas",
                columns: table => new
                {
                    TehnikaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TehnikaImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TehnikaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TehnikaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TehnikaVIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TehnikaYear = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tehnikas", x => x.TehnikaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servs_TehnikaId",
                table: "Servs",
                column: "TehnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_TehnikaId",
                table: "Insurances",
                column: "TehnikaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Tehnikas_TehnikaId",
                table: "Insurances",
                column: "TehnikaId",
                principalTable: "Tehnikas",
                principalColumn: "TehnikaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servs_Tehnikas_TehnikaId",
                table: "Servs",
                column: "TehnikaId",
                principalTable: "Tehnikas",
                principalColumn: "TehnikaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
