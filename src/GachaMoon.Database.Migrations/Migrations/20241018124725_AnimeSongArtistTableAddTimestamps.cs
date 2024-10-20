using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSongArtistTableAddTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "UpdatedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AnimeSongArtist");
        }
    }
}
