﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVGLClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSessionResultOptimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Optimal",
                table: "SessionResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optimal",
                table: "SessionResult");
        }
    }
}
