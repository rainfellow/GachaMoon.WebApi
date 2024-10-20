using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSongsArtistsModifyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsId",
                table: "AnimeSongAnimeSongArtist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimeSongArtist",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AnimeSongArtist");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AnimeSongArtist");

            migrationBuilder.RenameColumn(
                name: "AnimeSongArtistsId",
                table: "AnimeSongAnimeSongArtist",
                newName: "AnimeSongArtistsArtistId");

            migrationBuilder.AlterColumn<long>(
                name: "ArtistId",
                table: "AnimeSongArtist",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimeSongArtist",
                table: "AnimeSongArtist",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsAr~",
                table: "AnimeSongAnimeSongArtist",
                column: "AnimeSongArtistsArtistId",
                principalTable: "AnimeSongArtist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsAr~",
                table: "AnimeSongAnimeSongArtist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimeSongArtist",
                table: "AnimeSongArtist");

            migrationBuilder.RenameColumn(
                name: "AnimeSongArtistsArtistId",
                table: "AnimeSongAnimeSongArtist",
                newName: "AnimeSongArtistsId");

            migrationBuilder.AlterColumn<long>(
                name: "ArtistId",
                table: "AnimeSongArtist",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "AnimeSongArtist",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "UpdatedAt",
                table: "AnimeSongArtist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimeSongArtist",
                table: "AnimeSongArtist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeSongAnimeSongArtist_AnimeSongArtist_AnimeSongArtistsId",
                table: "AnimeSongAnimeSongArtist",
                column: "AnimeSongArtistsId",
                principalTable: "AnimeSongArtist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
