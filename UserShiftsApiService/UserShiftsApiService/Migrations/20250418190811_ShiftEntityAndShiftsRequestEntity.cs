using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class ShiftEntityAndShiftsRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    ShiftRequestType = table.Column<int>(type: "integer", nullable: false),
                    ShiftIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserShiftPreferenceRequests_UserId",
                table: "UserShiftPreferenceRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "UserShiftPreferenceRequests");
        }
    }
}
