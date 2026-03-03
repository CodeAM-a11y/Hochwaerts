using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hochwaerts.Migrations
{
    /// <inheritdoc />
    public partial class OneTOmanyOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verleihobjekt_Zauberer_ZaubererId",
                table: "Verleihobjekt");

            migrationBuilder.AlterColumn<int>(
                name: "ZaubererId",
                table: "Verleihobjekt",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Verleihobjekt_Zauberer_ZaubererId",
                table: "Verleihobjekt",
                column: "ZaubererId",
                principalTable: "Zauberer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verleihobjekt_Zauberer_ZaubererId",
                table: "Verleihobjekt");

            migrationBuilder.AlterColumn<int>(
                name: "ZaubererId",
                table: "Verleihobjekt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Verleihobjekt_Zauberer_ZaubererId",
                table: "Verleihobjekt",
                column: "ZaubererId",
                principalTable: "Zauberer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
