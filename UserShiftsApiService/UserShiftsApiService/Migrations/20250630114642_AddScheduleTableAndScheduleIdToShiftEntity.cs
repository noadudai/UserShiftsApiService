using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleTableAndScheduleIdToShiftEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScheduleId",
                table: "Shifts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShiftsSchedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByManagerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftsSchedules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ScheduleId",
                table: "Shifts",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_ShiftsSchedules_ScheduleId",
                table: "Shifts",
                column: "ScheduleId",
                principalTable: "ShiftsSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_ShiftsSchedules_ScheduleId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "ShiftsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ScheduleId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Shifts");
        }
    }
}
