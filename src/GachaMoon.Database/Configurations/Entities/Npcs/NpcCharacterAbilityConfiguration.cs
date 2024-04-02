// src/GachaMoon.Database/Configurations/Entities/Npcs/NpcCharacterAbilityConfiguration.cs
using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Npcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Npcs;
public class NpcCharacterAbilityConfiguration : IEntityTypeConfiguration<NpcCharacterAbility>
{
    public void Configure(EntityTypeBuilder<NpcCharacterAbility> builder)
    {
        _ = builder.Property(n => n.NpcCharacterId).IsRequired();
        _ = builder.Property(n => n.CharacterAbilityId).IsRequired();

        _ = builder.HasOne(n => n.NpcCharacter)
            .WithMany()
            .HasForeignKey(n => n.NpcCharacterId);
        _ = builder.HasOne(n => n.CharacterAbility)
            .WithMany()
            .HasForeignKey(n => n.CharacterAbilityId);

        _ = builder.HasIndex(n => new { n.NpcCharacterId, n.CharacterAbilityId }).WhereNotDeleted().IsUnique();
    }
}