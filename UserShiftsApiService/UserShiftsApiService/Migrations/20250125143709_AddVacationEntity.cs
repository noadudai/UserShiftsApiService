using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserShiftsApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddVacationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthSub = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDateRangeScheduleRequestEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDateRangeScheduleRequestEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDateRangeScheduleRequestEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDateRangeScheduleRequestEntity_UserId",
                table: "UserDateRangeScheduleRequestEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDateRangeScheduleRequestEntity");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
