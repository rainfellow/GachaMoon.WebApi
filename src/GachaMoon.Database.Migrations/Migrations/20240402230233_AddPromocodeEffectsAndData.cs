using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddPromocodeEffectsAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StandardBannerRollsAmount",
                table: "PremiumInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BannerExpiryDate",
                table: "Banners",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PromocodeEffects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PromoCodeId = table.Column<long>(type: "bigint", nullable: false),
                    EffectType = table.Column<int>(type: "integer", nullable: false),
                    EffectAmount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocodeEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromocodeEffects_Promocodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "Promocodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Аккихи");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Кадзуал");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Name",
                value: "Панкоед");

            migrationBuilder.InsertData(
                table: "Promocodes",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "ExpiryDate", "UpdatedAt", "UsesLeft" },
                values: new object[,]
                {
                    { 1L, "APRILFOOLS", NodaTime.Instant.FromUnixTimeTicks(0L), null, new DateOnly(1, 1, 1), NodaTime.Instant.FromUnixTimeTicks(0L), 0 },
                    { 2L, "SORRYFROZEN", NodaTime.Instant.FromUnixTimeTicks(0L), null, new DateOnly(1, 1, 1), NodaTime.Instant.FromUnixTimeTicks(0L), 0 },
                    { 3L, "GIVEMEROLLS", NodaTime.Instant.FromUnixTimeTicks(0L), null, new DateOnly(1, 1, 1), NodaTime.Instant.FromUnixTimeTicks(0L), 0 }
                });

            migrationBuilder.InsertData(
                table: "PromocodeEffects",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EffectAmount", "EffectType", "PromoCodeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, NodaTime.Instant.FromUnixTimeTicks(0L), null, 1000, 1, 1L, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 2L, NodaTime.Instant.FromUnixTimeTicks(0L), null, 1000, 1, 2L, NodaTime.Instant.FromUnixTimeTicks(0L) },
                    { 3L, NodaTime.Instant.FromUnixTimeTicks(0L), null, 10, 2, 3L, NodaTime.Instant.FromUnixTimeTicks(0L) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromocodeEffects_PromoCodeId_EffectType",
                table: "PromocodeEffects",
                columns: new[] { "PromoCodeId", "EffectType" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromocodeEffects");

            migrationBuilder.DeleteData(
                table: "Promocodes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Promocodes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Promocodes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "StandardBannerRollsAmount",
                table: "PremiumInventories");

            migrationBuilder.DropColumn(
                name: "BannerExpiryDate",
                table: "Banners");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Ромихи");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Пациент 163");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Name",
                value: "Кедонап");
        }
    }
}
