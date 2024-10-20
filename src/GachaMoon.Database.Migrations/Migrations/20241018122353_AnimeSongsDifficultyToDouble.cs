using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSongsDifficultyToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AMQSongDifficulty",
                table: "AnimeSong",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AnimeMusicQuizId",
                table: "Animes",
                column: "AnimeMusicQuizId",
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Animes_AnimeMusicQuizId",
                table: "Animes");

            migrationBuilder.AlterColumn<int>(
                name: "AMQSongDifficulty",
                table: "AnimeSong",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
