﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoService.DAL.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoImage",
                table: "Autos");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageAuto",
                table: "Autos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "Autos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAuto",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "Autos");

            migrationBuilder.AddColumn<string>(
                name: "AutoImage",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
