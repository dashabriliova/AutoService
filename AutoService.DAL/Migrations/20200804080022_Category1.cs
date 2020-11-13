using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class Category1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "Drivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
