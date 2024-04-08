using GachaMoon.Database.Configurations.Entities.Accounts;
using GachaMoon.Database.Configurations.Entities.Banners;
using GachaMoon.Database.Configurations.Entities.Characters;
using GachaMoon.Database.Configurations.Entities.Promocodes;
using GachaMoon.Database.Configurations.Entities.Users;
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Banners;
using GachaMoon.Domain.Characters;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Domain.Npcs;
using GachaMoon.Domain.Promocodes;
using GachaMoon.Domain.Users;
using GachaMoon.Services.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Database;

public class ApplicationDbContext(DbContextOptions options, IClockProvider clockProvider) : DbContext(options)
{
    private readonly IClockProvider _clockProvider = clockProvider;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateUpdatedAtProperty();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUpdatedAtProperty();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateUpdatedAtProperty()
    {
        var changedEntities = ChangeTracker.Entries()
                   .Where(x => x.State is EntityState.Modified or EntityState.Added)
                   .ToList();
        var timestamp = _clockProvider.Now;

        foreach (var entity in changedEntities)
        {
            var updatedAtProperty = entity.Property("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.CurrentValue = entity.State switch
                {
                    EntityState.Added => entity.Property("CreatedAt")?.CurrentValue ?? timestamp,
                    EntityState.Detached
                    or EntityState.Unchanged
                    or EntityState.Deleted
                    or EntityState.Modified => timestamp,
                    _ => throw new NotImplementedException(),
                };
            }
        }
    }

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new InternalUserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ExternalUserConfiguration());

        _ = modelBuilder.ApplyConfiguration(new AccountConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AccountBannerHistoryConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AccountBannerStatsConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AccountCharacterConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AccountCharacterAbilityConfiguration());
        _ = modelBuilder.ApplyConfiguration(new PremiumInventoryConfiguration());

        _ = modelBuilder.ApplyConfiguration(new BannerConfiguration());
        _ = modelBuilder.ApplyConfiguration(new BannerCharacterConfiguration());

        _ = modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        _ = modelBuilder.ApplyConfiguration(new CharacterAbilityConfiguration());
        _ = modelBuilder.ApplyConfiguration(new CharacterBaseStatsConfiguration());
        _ = modelBuilder.ApplyConfiguration(new DefaultCharacterAbilityConfiguration());

        _ = modelBuilder.ApplyConfiguration(new PromocodeConfiguration());
        _ = modelBuilder.ApplyConfiguration(new PromocodeEffectConfiguration());
        _ = modelBuilder.ApplyConfiguration(new PromocodeHistoryConfiguration());

        _ = modelBuilder.ApplyConfiguration(new AccountConnectedExternalServiceConfiguration());
    }

#nullable disable
    public DbSet<InternalUser> InternalUsers { get; set; }
    public DbSet<ExternalUser> ExternalUsers { get; set; }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountBannerHistory> AccountBannerHistory { get; set; }
    public DbSet<AccountBannerStats> AccountBannerStats { get; set; }
    public DbSet<AccountCharacter> AccountCharacters { get; set; }
    public DbSet<AccountCharacterAbility> AccountCharacterAbilities { get; set; }
    public DbSet<PremiumInventory> PremiumInventories { get; set; }

    public DbSet<Banner> Banners { get; set; }
    public DbSet<BannerCharacter> BannerCharacters { get; set; }

    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterAbility> CharacterAbilities { get; set; }
    public DbSet<CharacterBaseStats> CharacterBaseStats { get; set; }
    public DbSet<DefaultCharacterAbility> DefaultCharacterAbilities { get; set; }

    public DbSet<Promocode> Promocodes { get; set; }
    public DbSet<PromocodeEffect> PromocodeEffects { get; set; }
    public DbSet<PromocodeHistory> PromocodeHistory { get; set; }

    public DbSet<NpcCharacter> NpcCharacters { get; set; }
    public DbSet<NpcCharacterAbility> NpcCharacterAbilities { get; set; }
    public DbSet<NpcCharacterBaseStats> NpcCharacterBaseStats { get; set; }

    public DbSet<AccountConnectedExternalService> AccountExternalServices { get; set; }

#nullable enable
}
