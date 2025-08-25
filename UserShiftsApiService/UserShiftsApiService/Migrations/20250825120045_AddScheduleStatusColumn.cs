using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ShiftsSchedules",
                type: "text",
                nullable: false,
                defaultValue: "Draft");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftsSchedules_CreatedByManagerId",
                table: "ShiftsSchedules",
                column: "CreatedByManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftsSchedules_Users_CreatedByManagerId",
                table: "ShiftsSchedules",
                column: "CreatedByManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftsSchedules_Users_CreatedByManagerId",
                table: "ShiftsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ShiftsSchedules_CreatedByManagerId",
                table: "ShiftsSchedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShiftsSchedules");
        }
    }
}
