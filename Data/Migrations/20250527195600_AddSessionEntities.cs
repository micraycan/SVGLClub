using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVGLClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionCars_Sessions_AssettoSessionId",
                table: "SessionCars");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEvents_Sessions_AssettoSessionId",
                table: "SessionEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionLaps_Sessions_AssettoSessionId",
                table: "SessionLaps");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallastKG",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BestLap",
                table: "SessionResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "SessionResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverGuid",
                table: "SessionResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "SessionResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Restrictor",
                table: "SessionResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TotalTime",
                table: "SessionResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallastKG",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "SessionLaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Cuts",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriverGuid",
                table: "SessionLaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "SessionLaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LapTime",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Restrictor",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sector1",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sector2",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sector3",
                table: "SessionLaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Timestamp",
                table: "SessionLaps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Tyre",
                table: "SessionLaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "SessionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriverGuid",
                table: "SessionEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "SessionEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ImpactSpeed",
                table: "SessionEvents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "OtherCarId",
                table: "SessionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OtherDrivername",
                table: "SessionEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SessionEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionCars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BallastKG",
                table: "SessionCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "SessionCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriverGuid",
                table: "SessionCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "SessionCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "SessionCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Restrictor",
                table: "SessionCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Skin",
                table: "SessionCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCars_Sessions_AssettoSessionId",
                table: "SessionCars",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEvents_Sessions_AssettoSessionId",
                table: "SessionEvents",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionLaps_Sessions_AssettoSessionId",
                table: "SessionLaps",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionCars_Sessions_AssettoSessionId",
                table: "SessionCars");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEvents_Sessions_AssettoSessionId",
                table: "SessionEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionLaps_Sessions_AssettoSessionId",
                table: "SessionLaps");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "BallastKG",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "BestLap",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "DriverGuid",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "Restrictor",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "SessionResult");

            migrationBuilder.DropColumn(
                name: "BallastKG",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Cuts",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "DriverGuid",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "LapTime",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Restrictor",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Sector1",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Sector2",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Sector3",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "Tyre",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "DriverGuid",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "ImpactSpeed",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "OtherCarId",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "OtherDrivername",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SessionEvents");

            migrationBuilder.DropColumn(
                name: "BallastKG",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "DriverGuid",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "Restrictor",
                table: "SessionCars");

            migrationBuilder.DropColumn(
                name: "Skin",
                table: "SessionCars");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionResult",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionLaps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssettoSessionId",
                table: "SessionCars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCars_Sessions_AssettoSessionId",
                table: "SessionCars",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEvents_Sessions_AssettoSessionId",
                table: "SessionEvents",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionLaps_Sessions_AssettoSessionId",
                table: "SessionLaps",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionResult_Sessions_AssettoSessionId",
                table: "SessionResult",
                column: "AssettoSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
