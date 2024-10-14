using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimesAddMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeRating",
                table: "Animes",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "ERR");

            migrationBuilder.AddColumn<string>(
                name: "AnimeType",
                table: "Animes",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "ERR");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeCount",
                table: "Animes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MeanScore",
                table: "Animes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Animes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "ERR");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeNumber",
                table: "AnimeEpisodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRating",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "AnimeType",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "EpisodeCount",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "MeanScore",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "AnimeEpisodes");
        }
    }
}
