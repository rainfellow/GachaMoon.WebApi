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
        builder.Property(n => n.NpcCharacterId).IsRequired();
        builder.Property(n => n.CharacterAbilityId).IsRequired();

        builder.HasOne(n => n.NpcCharacter)
            .WithMany()
            .HasForeignKey(n => n.NpcCharacterId);
        builder.HasOne(n => n.CharacterAbility)
            .WithMany()
            .HasForeignKey(n => n.CharacterAbilityId);

        builder.HasIndex(n => new { n.NpcCharacterId, n.CharacterAbilityId }).WhereNotDeleted().IsUnique();
    }
}