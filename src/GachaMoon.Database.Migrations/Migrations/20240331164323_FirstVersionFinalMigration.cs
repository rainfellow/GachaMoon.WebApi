using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FirstVersionFinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PremiumCurrency",
                table: "PremiumInventories",
                newName: "WildcardSkillItemCount");

            migrationBuilder.AddColumn<int>(
                name: "PremiumCurrencyAmount",
                table: "PremiumInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityRange",
                table: "CharacterAbilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalEpicRolls",
                table: "AccountBannerStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLegendaryRolls",
                table: "AccountBannerStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "CharacterAbilities",
                columns: new[] { "Id", "AbilityRange", "AbilityTarget", "AbilityType", "CreatedAt", "DeletedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1, 3, 1, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Эта атака является затычкой, и не должна появляться у персонажа.", "Placeholder: basic attack", NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 2L, 1, 3, 2, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Эта атака является затычкой, и не должна появляться у персонажа.", "Placeholder: special attack", NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 3L, 2, 3, 3, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Эта атака является затычкой, и не должна появляться у персонажа.", "Placeholder: ultimate attack", NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 4L, 0, 0, 4, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Это умение является затычкой, и не должно появляться у персонажа.", "Placeholder: passive skill", NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 5L, 1, 3, 1, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Бросает сюрикен в указанного противника, нанося урон в зависимости от силы атаки персонажа.", "Бросок сюрикена", NodaTime.Instant.FromUnixTimeTicks(0L) }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "CharacterType", "CreatedAt", "DeletedAt", "Name", "Rarity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Ромихи", 2, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 2L, 2, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Шувидор", 2, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 3L, 4, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Пациент 163", 2, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 4L, 3, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Чехов", 1, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 5L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Черная Мамба", 1, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 6L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Яна Цист", 1, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 7L, 4, NodaTime.Instant.FromUnixTimeTicks(0L), null, "Кедонап", 1, NodaTime.Instant.FromUnixTimeTicks(0L) }
                });

            migrationBuilder.InsertData(
                table: "CharacterBaseStats",
                columns: new[] { "Id", "Attack", "CharacterId", "CharacterLevel", "CreatedAt", "Defence", "DeletedAt", "Health", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 150, 1L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 80, null, 100, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 2L, 100, 2L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 140, null, 100, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 3L, 160, 3L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 70, null, 100, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 4L, 60, 4L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 100, null, 140, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 5L, 140, 5L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 50, null, 90, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 6L, 120, 6L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 70, null, 100, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 7L, 130, 7L, 1, NodaTime.Instant.FromUnixTimeTicks(0L), 90, null, 70, NodaTime.Instant.FromUnixTimeTicks(0L) }
                });

            migrationBuilder.InsertData(
                table: "DefaultCharacterAbilities",
                columns: new[] { "Id", "AbilityType", "CharacterAbilityId", "CharacterId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1, 1L, 1L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 2L, 2, 2L, 1L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 3L, 3, 3L, 1L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 4L, 4, 4L, 1L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 5L, 1, 1L, 2L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 6L, 2, 2L, 2L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 7L, 3, 3L, 2L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 8L, 4, 4L, 2L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 9L, 1, 1L, 3L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 10L, 2, 2L, 3L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 11L, 3, 3L, 3L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 12L, 4, 4L, 3L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 13L, 1, 1L, 4L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 14L, 2, 2L, 4L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 15L, 3, 3L, 4L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 16L, 4, 4L, 4L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 17L, 1, 1L, 5L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 18L, 2, 2L, 5L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 19L, 3, 3L, 5L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 20L, 4, 4L, 5L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 21L, 1, 5L, 6L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 22L, 2, 2L, 6L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 23L, 3, 3L, 6L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 24L, 4, 4L, 6L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 25L, 1, 1L, 7L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 26L, 2, 2L, 7L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 27L, 3, 3L, 7L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 28L, 4, 4L, 7L, NodaTime.Instant.FromUnixTimeTicks(0L), null, NodaTime.Instant.FromUnixTimeTicks(0L) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_UserType_Identifier",
                table: "ExternalUsers",
                columns: new[] { "UserType", "Identifier" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalUsers_UserType_Identifier",
                table: "ExternalUsers");

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DropColumn(
                name: "PremiumCurrencyAmount",
                table: "PremiumInventories");

            migrationBuilder.DropColumn(
                name: "AbilityRange",
                table: "CharacterAbilities");

            migrationBuilder.DropColumn(
                name: "TotalEpicRolls",
                table: "AccountBannerStats");

            migrationBuilder.DropColumn(
                name: "TotalLegendaryRolls",
                table: "AccountBannerStats");

            migrationBuilder.RenameColumn(
                name: "WildcardSkillItemCount",
                table: "PremiumInventories",
                newName: "PremiumCurrency");
        }
    }
}
