using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hochwaerts.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bibliothek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliothek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titel = table.Column<string>(type: "TEXT", nullable: false),
                    Inhalt = table.Column<string>(type: "TEXT", nullable: false),
                    Erscheinungsjahr = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", nullable: false),
                    Beschaedigungsgrad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Zustand = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zauberer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Haus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zauberer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BuchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autor_Buch_BuchId",
                        column: x => x.BuchId,
                        principalTable: "Buch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verleihobjekt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    BibliothekId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuchID = table.Column<int>(type: "INTEGER", nullable: false),
                    ZaubererId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verleihobjekt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verleihobjekt_Bibliothek_BibliothekId",
                        column: x => x.BibliothekId,
                        principalTable: "Bibliothek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verleihobjekt_Buch_BuchID",
                        column: x => x.BuchID,
                        principalTable: "Buch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verleihobjekt_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verleihobjekt_Zauberer_ZaubererId",
                        column: x => x.ZaubererId,
                        principalTable: "Zauberer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autor_BuchId",
                table: "Autor",
                column: "BuchId");

            migrationBuilder.CreateIndex(
                name: "IX_Verleihobjekt_BibliothekId",
                table: "Verleihobjekt",
                column: "BibliothekId");

            migrationBuilder.CreateIndex(
                name: "IX_Verleihobjekt_BuchID",
                table: "Verleihobjekt",
                column: "BuchID");

            migrationBuilder.CreateIndex(
                name: "IX_Verleihobjekt_StatusId",
                table: "Verleihobjekt",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Verleihobjekt_ZaubererId",
                table: "Verleihobjekt",
                column: "ZaubererId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Verleihobjekt");

            migrationBuilder.DropTable(
                name: "Bibliothek");

            migrationBuilder.DropTable(
                name: "Buch");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Zauberer");
        }
    }
}
