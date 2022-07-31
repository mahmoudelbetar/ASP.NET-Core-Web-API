using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkyAPI.Migrations
{
    public partial class addImagetoNP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "NationalPark",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "NationalPark");
        }
    }
}
