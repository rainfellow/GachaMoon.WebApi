using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSongsAddSongType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongType",
                table: "AnimeSong",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongType",
                table: "AnimeSong");
        }
    }
}
