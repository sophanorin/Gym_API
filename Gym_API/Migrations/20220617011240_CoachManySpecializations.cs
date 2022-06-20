using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_API.Migrations
{
    public partial class CoachManySpecializations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coach_Specializations_SpecializationId",
                table: "Coach");

            migrationBuilder.DropIndex(
                name: "IX_Coach_SpecializationId",
                table: "Coach");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Coach");

            migrationBuilder.CreateTable(
                name: "CoachSpecialization",
                columns: table => new
                {
                    CoachesId = table.Column<string>(type: "TEXT", nullable: false),
                    SpecializationsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachSpecialization", x => new { x.CoachesId, x.SpecializationsId });
                    table.ForeignKey(
                        name: "FK_CoachSpecialization_Coach_CoachesId",
                        column: x => x.CoachesId,
                        principalTable: "Coach",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachSpecialization_Specializations_SpecializationsId",
                        column: x => x.SpecializationsId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachSpecialization_SpecializationsId",
                table: "CoachSpecialization",
                column: "SpecializationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachSpecialization");

            migrationBuilder.AddColumn<string>(
                name: "SpecializationId",
                table: "Coach",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coach_SpecializationId",
                table: "Coach",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coach_Specializations_SpecializationId",
                table: "Coach",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
