using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeSongsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageSiteTitle",
                table: "Animes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "AnilistId",
                table: "Animes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasImages",
                table: "Animes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSongs",
                table: "Animes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AnimeSong",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SongTypeString = table.Column<string>(type: "text", nullable: true),
                    SongArtist = table.Column<string>(type: "text", nullable: true),
                    AMQSongDifficulty = table.Column<int>(type: "integer", nullable: false),
                    AMQSongCategory = table.Column<string>(type: "text", nullable: false),
                    CatboxAudioLink = table.Column<string>(type: "text", nullable: true),
                    CatboxMQLink = table.Column<string>(type: "text", nullable: true),
                    CatboxHQLink = table.Column<string>(type: "text", nullable: true),
                    OpeningsMoeLink = table.Column<string>(type: "text", nullable: true),
                    GachamoonLink = table.Column<string>(type: "text", nullable: true),
                    AnimeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeSong_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSongArtist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    ArtistName = table.Column<string>(type: "text", nullable: false),
                    ArtistType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSongArtist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSongAnimeSongArtist",
                columns: table => new
                {
                    AnimeSongArtistsId = table.Column<long>(type: "bigint", nullable: false),
                    AnimeSongsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSongAnimeSongArtist", x => new { x.AnimeSongArtistsId, x.AnimeSongsId });
                    table.ForeignKey(
                        name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsId",
                        column: x => x.AnimeSongArtistsId,
                        principalTable: "AnimeSongArtist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeSongAnimeSongArtist_AnimeSong_AnimeSongsId",
                        column: x => x.AnimeSongsId,
                        principalTable: "AnimeSong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSong_AnimeId",
                table: "AnimeSong",
                column: "AnimeId");

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

            migrationBuilder.DropTable(
                name: "AnimeSongArtist");

            migrationBuilder.DropTable(
                name: "AnimeSong");

            migrationBuilder.DropColumn(
                name: "HasImages",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "HasSongs",
                table: "Animes");

            migrationBuilder.AlterColumn<string>(
                name: "ImageSiteTitle",
                table: "Animes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnilistId",
                table: "Animes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
