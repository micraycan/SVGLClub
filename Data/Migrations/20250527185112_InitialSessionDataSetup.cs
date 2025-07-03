using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVGLClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSessionDataSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laps_SessionResults_SessionResultId",
                table: "Laps");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionResults_Drivers_DriverId",
                table: "SessionResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionResults",
                table: "SessionResults");

            migrationBuilder.RenameTable(
                name: "SessionResults",
                newName: "SessionResult");

            migrationBuilder.RenameIndex(
                name: "IX_SessionResults_DriverId",
                table: "SessionResult",
                newName: "IX_SessionResult_DriverId");

            migrationBuilder.AddColumn<int>(
                name: "AssettoSessionId",
                table: "SessionResult",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionResult",
                table: "SessionResult",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackConfig = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationSecs = table.Column<int>(type: "int", nullable: false),
                    RaceLaps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssettoSessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionCars_Sessions_AssettoSessionId",
                        column: x => x.AssettoSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssettoSessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionEvents_Sessions_AssettoSessionId",
                        column: x => x.AssettoSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionLaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssettoSessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionLaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionLaps_Sessions_AssettoSessionId",
                        column: x => x.AssettoSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionResult_AssettoSessionId",
                table: "SessionResult",
                column: "AssettoSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionCars_AssettoSessionId",
                table: "SessionCars",
                column: "AssettoSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEvents_AssettoSessionId",
                table: "SessionEvents",
                column: "AssettoSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_AssettoSessionId",
                table: "SessionLaps",
                column: "AssettoSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laps_SessionResult_SessionResultId",
                table: "Laps",
                column: "SessionResultId",
                principalTable: "SessionResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResult_Drivers_DriverId",
                table: "SessionResult",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laps_SessionResult_SessionResultId",
                table: "Laps");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionResult_Drivers_DriverId",
                table: "SessionResult");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult");

            migrationBuilder.DropTable(
                name: "SessionCars");

            migrationBuilder.DropTable(
                name: "SessionEvents");

            migrationBuilder.DropTable(
                name: "SessionLaps");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionResult",
                table: "SessionResult");

            migrationBuilder.DropIndex(
                name: "IX_SessionResult_AssettoSessionId",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "AssettoSessionId",
                table: "SessionResult");

            migrationBuilder.RenameTable(
                name: "SessionResult",
                newName: "SessionResults");

            migrationBuilder.RenameIndex(
                name: "IX_SessionResult_DriverId",
                table: "SessionResults",
                newName: "IX_SessionResults_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionResults",
                table: "SessionResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laps_SessionResults_SessionResultId",
                table: "Laps",
                column: "SessionResultId",
                principalTable: "SessionResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResults_Drivers_DriverId",
                table: "SessionResults",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
