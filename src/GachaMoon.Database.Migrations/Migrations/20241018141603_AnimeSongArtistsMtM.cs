using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSongArtistsMtM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeSongAnimeSongArtist",
                columns: table => new
                {
                    AnimeSongArtistsArtistId = table.Column<long>(type: "bigint", nullable: false),
                    AnimeSongsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSongAnimeSongArtist", x => new { x.AnimeSongArtistsArtistId, x.AnimeSongsId });
                    table.ForeignKey(
                        name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsAr~",
                        column: x => x.AnimeSongArtistsArtistId,
                        principalTable: "AnimeSongArtist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeSongAnimeSongArtist_AnimeSong_AnimeSongsId",
                        column: x => x.AnimeSongsId,
                        principalTable: "AnimeSong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSongAnimeSongArtist_AnimeSongsId",
                table: "AnimeSongAnimeSongArtist",
                column: "AnimeSongsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeSongAnimeSongArtist");
        }
    }
}
