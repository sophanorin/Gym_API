using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class AddSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_AspNetUsers_UserCredentialId",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Supervisor_UserCredentialId",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCredentialId",
                table: "Supervisor");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "AspNetUsers",
                newName: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SupervisorId",
                table: "AspNetUsers",
                column: "SupervisorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Supervisor_SupervisorId",
                table: "AspNetUsers",
                column: "SupervisorId",
                principalTable: "Supervisor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Supervisor_SupervisorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SupervisorId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "AspNetUsers",
                newName: "RoleId");

            migrationBuilder.AddColumn<string>(
                name: "UserCredentialId",
                table: "Supervisor",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_UserCredentialId",
                table: "Supervisor",
                column: "UserCredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_AspNetUsers_UserCredentialId",
                table: "Supervisor",
                column: "UserCredentialId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
