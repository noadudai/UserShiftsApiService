using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class ShiftsRequestEntityString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShiftRequestType",
                table: "UserShiftPreferenceRequests",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ShiftRequestType",
                table: "UserShiftPreferenceRequests",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
