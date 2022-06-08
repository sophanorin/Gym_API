using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class CascadePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coach_Genders_GenderId",
                table: "Coach");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Genders_GenderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Genders_GenderId",
                table: "Supervisor");

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Supervisor",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Coach",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coach_Genders_GenderId",
                table: "Coach",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Genders_GenderId",
                table: "Customer",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Genders_GenderId",
                table: "Supervisor",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coach_Genders_GenderId",
                table: "Coach");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Genders_GenderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Genders_GenderId",
                table: "Supervisor");

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Supervisor",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Customer",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GenderId",
                table: "Coach",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Coach_Genders_GenderId",
                table: "Coach",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Genders_GenderId",
                table: "Customer",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Genders_GenderId",
                table: "Supervisor",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");
        }
    }
}
