using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftAndShiftRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedShift_UserShiftPreferenceRequests_UserShiftsReques~",
                table: "RequestedShift");

            migrationBuilder.RenameColumn(
                name: "UserShiftsRequestId",
                table: "RequestedShift",
                newName: "UserShiftsPreferenceRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedShift_UserShiftsRequestId",
                table: "RequestedShift",
                newName: "IX_RequestedShift_UserShiftsPreferenceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedShift_UserShiftPreferenceRequests_UserShiftsPrefer~",
                table: "RequestedShift",
                column: "UserShiftsPreferenceRequestId",
                principalTable: "UserShiftPreferenceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedShift_UserShiftPreferenceRequests_UserShiftsPrefer~",
                table: "RequestedShift");

            migrationBuilder.RenameColumn(
                name: "UserShiftsPreferenceRequestId",
                table: "RequestedShift",
                newName: "UserShiftsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedShift_UserShiftsPreferenceRequestId",
                table: "RequestedShift",
                newName: "IX_RequestedShift_UserShiftsRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedShift_UserShiftPreferenceRequests_UserShiftsReques~",
                table: "RequestedShift",
                column: "UserShiftsRequestId",
                principalTable: "UserShiftPreferenceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
