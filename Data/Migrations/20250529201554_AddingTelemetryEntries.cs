using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVGLClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingTelemetryEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optimal",
                table: "SessionResult");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TelemetryEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Driver = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackVariation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Car = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LapTimeMs = table.Column<int>(type: "int", nullable: false),
                    NumDataPoints = table.Column<int>(type: "int", nullable: false),
                    PositionJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GearJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpeedJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThrottleJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrakeJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Filename",
                table: "Sessions",
                column: "Filename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEntries_Driver_Track_TrackVariation_Car_LapTimeMs",
                table: "TelemetryEntries",
                columns: new[] { "Driver", "Track", "TrackVariation", "Car", "LapTimeMs" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelemetryEntries");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_Filename",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Optimal",
                table: "SessionResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
