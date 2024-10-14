using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AnimeAliasesFixRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimeAliases_AnimeId",
                table: "AnimeAliases",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAliases_Animes_AnimeId",
                table: "AnimeAliases",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAliases_Animes_AnimeId",
                table: "AnimeAliases");

            migrationBuilder.DropIndex(
                name: "IX_AnimeAliases_AnimeId",
                table: "AnimeAliases");
        }
    }
}
