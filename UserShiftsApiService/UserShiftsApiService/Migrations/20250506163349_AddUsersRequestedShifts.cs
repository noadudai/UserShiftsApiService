using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersRequestedShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftIds",
                table: "UserShiftPreferenceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Shifts",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestedShift");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ShiftIds",
                table: "UserShiftPreferenceRequests",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Shifts",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
