using System;
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
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ShiftType = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserShiftPreferenceRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ShiftRequestType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShiftPreferenceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShiftPreferenceRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestedShift",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserShiftsRequestId = table.Column<string>(type: "text", nullable: false),
                    ShiftId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedShift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedShift_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestedShift_UserShiftPreferenceRequests_UserShiftsReques~",
                        column: x => x.UserShiftsRequestId,
                        principalTable: "UserShiftPreferenceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestedShift_ShiftId",
                table: "RequestedShift",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedShift_UserShiftsRequestId",
                table: "RequestedShift",
                column: "UserShiftsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserShiftPreferenceRequests_UserId",
                table: "UserShiftPreferenceRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestedShift");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "UserShiftPreferenceRequests");
        }
    }
}
