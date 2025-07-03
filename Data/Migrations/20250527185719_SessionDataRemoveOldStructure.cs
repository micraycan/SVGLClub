using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVGLClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SessionDataRemoveOldStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionResult_Drivers_DriverId",
                table: "SessionResult");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Laps");

            migrationBuilder.DropIndex(
                name: "IX_SessionResult_DriverId",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "Track",
                table: "SessionResult");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Track",
                table: "SessionResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SteamId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionResultId = table.Column<int>(type: "int", nullable: false),
                    LapTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laps_SessionResult_SessionResultId",
                        column: x => x.SessionResultId,
                        principalTable: "SessionResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionResult_DriverId",
                table: "SessionResult",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Laps_SessionResultId",
                table: "Laps",
                column: "SessionResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResult_Drivers_DriverId",
                table: "SessionResult",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
