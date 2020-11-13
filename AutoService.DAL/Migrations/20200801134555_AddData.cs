using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceDateProlongation",
                table: "Insurances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InspectionMonthNext",
                table: "Inspections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceDateProlongation",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "InspectionMonthNext",
                table: "Inspections");
        }
    }
}
