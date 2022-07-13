using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class AddGroupFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "Group",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Limitation",
                table: "Group",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenDate",
                table: "Group",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Limitation",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "OpenDate",
                table: "Group");
        }
    }
}
