using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class UpdateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "AspNetUsers",
                newName: "IsStuff");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsStuff",
                table: "AspNetUsers",
                newName: "IsAdmin");
        }
    }
}
