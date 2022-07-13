using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class AddUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Supervisor",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Customer",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Coach",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Supervisor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Coach",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Coach");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Supervisor",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Customer",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Coach",
                newName: "Fullname");
        }
    }
}
