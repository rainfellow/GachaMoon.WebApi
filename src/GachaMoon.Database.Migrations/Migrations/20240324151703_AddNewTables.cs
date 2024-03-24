using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GachaMoon.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AccountType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    AbilityType = table.Column<int>(type: "integer", nullable: false),
                    AbilityTarget = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    CharacterType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NpcCharacters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NpcType = table.Column<int>(type: "integer", nullable: false),
                    ChallengeRating = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcCharacters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UsesLeft = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountBannerStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    BannerType = table.Column<int>(type: "integer", nullable: false),
                    TotalRolls = table.Column<int>(type: "integer", nullable: false),
                    RollsToLegendary = table.Column<int>(type: "integer", nullable: false),
                    RollsToEpic = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBannerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBannerStats_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Identifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalUsers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 80, nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalUsers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PremiumInventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    PremiumCurrency = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PremiumInventories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountBannerHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    BannerId = table.Column<long>(type: "bigint", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBannerHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBannerHistory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBannerHistory_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCharacters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterLevel = table.Column<int>(type: "integer", nullable: false),
                    CharacterExperience = table.Column<int>(type: "integer", nullable: false),
                    RepeatCount = table.Column<int>(type: "integer", nullable: false),
                    TotalSkillPoints = table.Column<int>(type: "integer", nullable: false),
                    FreeSkillPoints = table.Column<int>(type: "integer", nullable: false),
                    SkillTree = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountCharacters_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannerCharacters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerCharacters_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannerCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterBaseStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterLevel = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defence = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterBaseStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterBaseStats_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaultCharacterAbilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterAbilityId = table.Column<long>(type: "bigint", nullable: false),
                    AbilityType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultCharacterAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaultCharacterAbilities_CharacterAbilities_CharacterAbili~",
                        column: x => x.CharacterAbilityId,
                        principalTable: "CharacterAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefaultCharacterAbilities_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NpcCharacterAbilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NpcCharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterAbilityId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcCharacterAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NpcCharacterAbilities_CharacterAbilities_CharacterAbilityId",
                        column: x => x.CharacterAbilityId,
                        principalTable: "CharacterAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NpcCharacterAbilities_NpcCharacters_NpcCharacterId",
                        column: x => x.NpcCharacterId,
                        principalTable: "NpcCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NpcCharacterBaseStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NpcCharacterId = table.Column<long>(type: "bigint", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defence = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcCharacterBaseStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NpcCharacterBaseStats_NpcCharacters_NpcCharacterId",
                        column: x => x.NpcCharacterId,
                        principalTable: "NpcCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocodeHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    PromoCodeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocodeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromocodeHistory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocodeHistory_Promocodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "Promocodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCharacterAbilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountCharacterId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterAbilityId = table.Column<long>(type: "bigint", nullable: false),
                    AbilityType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCharacterAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountCharacterAbilities_AccountCharacters_AccountCharacte~",
                        column: x => x.AccountCharacterId,
                        principalTable: "AccountCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCharacterAbilities_CharacterAbilities_CharacterAbili~",
                        column: x => x.CharacterAbilityId,
                        principalTable: "CharacterAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBannerHistory_AccountId_BannerId",
                table: "AccountBannerHistory",
                columns: new[] { "AccountId", "BannerId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBannerHistory_BannerId",
                table: "AccountBannerHistory",
                column: "BannerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBannerStats_AccountId_BannerType",
                table: "AccountBannerStats",
                columns: new[] { "AccountId", "BannerType" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCharacterAbilities_AbilityType_AccountCharacterId",
                table: "AccountCharacterAbilities",
                columns: new[] { "AbilityType", "AccountCharacterId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCharacterAbilities_AccountCharacterId",
                table: "AccountCharacterAbilities",
                column: "AccountCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCharacterAbilities_CharacterAbilityId",
                table: "AccountCharacterAbilities",
                column: "CharacterAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCharacters_AccountId_CharacterId",
                table: "AccountCharacters",
                columns: new[] { "AccountId", "CharacterId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCharacters_CharacterId",
                table: "AccountCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_BannerCharacters_BannerId",
                table: "BannerCharacters",
                column: "BannerId");

            migrationBuilder.CreateIndex(
                name: "IX_BannerCharacters_CharacterId",
                table: "BannerCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterBaseStats_CharacterId_CharacterLevel",
                table: "CharacterBaseStats",
                columns: new[] { "CharacterId", "CharacterLevel" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultCharacterAbilities_CharacterAbilityId",
                table: "DefaultCharacterAbilities",
                column: "CharacterAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultCharacterAbilities_CharacterId_AbilityType",
                table: "DefaultCharacterAbilities",
                columns: new[] { "CharacterId", "AbilityType" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_AccountId",
                table: "ExternalUsers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_UserType_AccountId",
                table: "ExternalUsers",
                columns: new[] { "UserType", "AccountId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalUsers_AccountId",
                table: "InternalUsers",
                column: "AccountId",
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCharacterAbilities_CharacterAbilityId",
                table: "NpcCharacterAbilities",
                column: "CharacterAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCharacterAbilities_NpcCharacterId",
                table: "NpcCharacterAbilities",
                column: "NpcCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCharacterBaseStats_NpcCharacterId",
                table: "NpcCharacterBaseStats",
                column: "NpcCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumInventories_AccountId",
                table: "PremiumInventories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PromocodeHistory_AccountId_PromoCodeId",
                table: "PromocodeHistory",
                columns: new[] { "AccountId", "PromoCodeId" },
                unique: true,
                filter: "\"DeletedAt\" is NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PromocodeHistory_PromoCodeId",
                table: "PromocodeHistory",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_Code",
                table: "Promocodes",
                column: "Code",
                unique: true,
                filter: "\"DeletedAt\" is NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBannerHistory");

            migrationBuilder.DropTable(
                name: "AccountBannerStats");

            migrationBuilder.DropTable(
                name: "AccountCharacterAbilities");

            migrationBuilder.DropTable(
                name: "BannerCharacters");

            migrationBuilder.DropTable(
                name: "CharacterBaseStats");

            migrationBuilder.DropTable(
                name: "DefaultCharacterAbilities");

            migrationBuilder.DropTable(
                name: "ExternalUsers");

            migrationBuilder.DropTable(
                name: "InternalUsers");

            migrationBuilder.DropTable(
                name: "NpcCharacterAbilities");

            migrationBuilder.DropTable(
                name: "NpcCharacterBaseStats");

            migrationBuilder.DropTable(
                name: "PremiumInventories");

            migrationBuilder.DropTable(
                name: "PromocodeHistory");

            migrationBuilder.DropTable(
                name: "AccountCharacters");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "NpcCharacters");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
