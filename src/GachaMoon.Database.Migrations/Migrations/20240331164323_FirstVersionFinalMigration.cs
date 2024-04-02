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
            _ = migrationBuilder.RenameColumn(
                name: "PremiumCurrency",
                table: "PremiumInventories",
                newName: "WildcardSkillItemCount");

            _ = migrationBuilder.AddColumn<int>(
                name: "PremiumCurrencyAmount",
                table: "PremiumInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<int>(
                name: "AbilityRange",
                table: "CharacterAbilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<int>(
                name: "TotalEpicRolls",
                table: "AccountBannerStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<int>(
                name: "TotalLegendaryRolls",
                table: "AccountBannerStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.InsertData(
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

            _ = migrationBuilder.InsertData(
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

            _ = migrationBuilder.InsertData(
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

            _ = migrationBuilder.InsertData(
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

            _ = migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_UserType_Identifier",
                table: "ExternalUsers",
                columns: new[] { "UserType", "Identifier" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropIndex(
                name: "IX_ExternalUsers_UserType_Identifier",
                table: "ExternalUsers");

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 1L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 2L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 3L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 4L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 5L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 6L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterBaseStats",
                keyColumn: "Id",
                keyValue: 7L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 1L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 2L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 3L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 4L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 5L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 6L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 7L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 8L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 9L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 10L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 11L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 12L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 13L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 14L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 15L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 16L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 17L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 18L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 19L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 20L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 21L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 22L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 23L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 24L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 25L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 26L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 27L);

            _ = migrationBuilder.DeleteData(
                table: "DefaultCharacterAbilities",
                keyColumn: "Id",
                keyValue: 28L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 1L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 2L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 3L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 4L);

            _ = migrationBuilder.DeleteData(
                table: "CharacterAbilities",
                keyColumn: "Id",
                keyValue: 5L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6L);

            _ = migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7L);

            _ = migrationBuilder.DropColumn(
                name: "PremiumCurrencyAmount",
                table: "PremiumInventories");

            _ = migrationBuilder.DropColumn(
                name: "AbilityRange",
                table: "CharacterAbilities");

            _ = migrationBuilder.DropColumn(
                name: "TotalEpicRolls",
                table: "AccountBannerStats");

            _ = migrationBuilder.DropColumn(
                name: "TotalLegendaryRolls",
                table: "AccountBannerStats");

            _ = migrationBuilder.RenameColumn(
                name: "WildcardSkillItemCount",
                table: "PremiumInventories",
                newName: "PremiumCurrency");
        }
    }
}
