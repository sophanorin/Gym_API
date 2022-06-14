using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class OptionalColumnGroupModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Coach_TrainerId",
                table: "Group");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Group",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Coach_TrainerId",
                table: "Group",
                column: "TrainerId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Coach_TrainerId",
                table: "Group");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Group",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Coach_TrainerId",
                table: "Group",
                column: "TrainerId",
                principalTable: "Coach",
                principalColumn: "Id");
        }
    }
}
