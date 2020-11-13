using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    AutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoName = table.Column<string>(nullable: true),
                    AutoNumber = table.Column<string>(nullable: true),
                    AutoYear = table.Column<DateTime>(nullable: false),
                    AutoVIN = table.Column<string>(nullable: true),
                    AutoImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.AutoId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverFullName = table.Column<string>(nullable: true),
                    DriverDB = table.Column<DateTime>(nullable: false),
                    DriverImage = table.Column<string>(nullable: true),
                    DriverCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Tehnikas",
                columns: table => new
                {
                    TehnikaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TehnikaName = table.Column<string>(nullable: true),
                    TehnikaNumber = table.Column<string>(nullable: true),
                    TehnikaYear = table.Column<DateTime>(nullable: false),
                    TehnikaVIN = table.Column<string>(nullable: true),
                    TehnikaImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tehnikas", x => x.TehnikaId);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    InspectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionDate = table.Column<DateTime>(nullable: false),
                    AutoId = table.Column<int>(nullable: false),
                    TehnikaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.InspectionId);
                    table.ForeignKey(
                        name: "FK_Inspections_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "AutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inspections_Tehnikas_TehnikaId",
                        column: x => x.TehnikaId,
                        principalTable: "Tehnikas",
                        principalColumn: "TehnikaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceDate = table.Column<DateTime>(nullable: false),
                    AutoId = table.Column<int>(nullable: false),
                    TehnikaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceId);
                    table.ForeignKey(
                        name: "FK_Insurances_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "AutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insurances_Tehnikas_TehnikaId",
                        column: x => x.TehnikaId,
                        principalTable: "Tehnikas",
                        principalColumn: "TehnikaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servs",
                columns: table => new
                {
                    ServId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServDate = table.Column<DateTime>(nullable: false),
                    ServNaim = table.Column<string>(nullable: true),
                    ServPokaz = table.Column<int>(nullable: false),
                    AutoId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: false),
                    TehnikaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servs", x => x.ServId);
                    table.ForeignKey(
                        name: "FK_Servs_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "AutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servs_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servs_Tehnikas_TehnikaId",
                        column: x => x.TehnikaId,
                        principalTable: "Tehnikas",
                        principalColumn: "TehnikaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_AutoId",
                table: "Inspections",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_TehnikaId",
                table: "Inspections",
                column: "TehnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_AutoId",
                table: "Insurances",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_TehnikaId",
                table: "Insurances",
                column: "TehnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servs_AutoId",
                table: "Servs",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servs_DriverId",
                table: "Servs",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Servs_TehnikaId",
                table: "Servs",
                column: "TehnikaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Servs");

            migrationBuilder.DropTable(
                name: "Autos");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Tehnikas");
        }
    }
}
