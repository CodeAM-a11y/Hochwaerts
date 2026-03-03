using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hochwaerts.Migrations
{
    /// <inheritdoc />
    public partial class VerleihDatum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Verleihdatum",
                table: "Verleihobjekt",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verleihdatum",
                table: "Verleihobjekt");
        }
    }
}
