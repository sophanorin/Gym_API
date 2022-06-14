using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class UpdateOneToManyCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Group_GroupId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Group_GroupId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GroupId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Group",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                columns: table => new
                {
                    CustomersId = table.Column<string>(type: "TEXT", nullable: false),
                    GroupsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroup", x => new { x.CustomersId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_CustomerGroup_Customer_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerGroup_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroup_GroupsId",
                table: "CustomerGroup",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Group_GroupId",
                table: "Schedule",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Group_GroupId",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "CustomerGroup");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Group",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GroupId",
                table: "Customer",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Group_GroupId",
                table: "Customer",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Group_GroupId",
                table: "Schedule",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }
    }
}
