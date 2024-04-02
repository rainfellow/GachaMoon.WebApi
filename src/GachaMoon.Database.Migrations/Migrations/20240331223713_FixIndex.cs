using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropIndex(
                name: "IX_AccountBannerHistory_AccountId_BannerId",
                table: "AccountBannerHistory");

            _ = migrationBuilder.CreateIndex(
                name: "IX_AccountBannerHistory_AccountId",
                table: "AccountBannerHistory",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropIndex(
                name: "IX_AccountBannerHistory_AccountId",
                table: "AccountBannerHistory");

            _ = migrationBuilder.CreateIndex(
                name: "IX_AccountBannerHistory_AccountId_BannerId",
                table: "AccountBannerHistory",
                columns: new[] { "AccountId", "BannerId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }
    }
}
