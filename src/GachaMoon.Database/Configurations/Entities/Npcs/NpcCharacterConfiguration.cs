// src/GachaMoon.Database/Configurations/NpcCharacterConfiguration.cs
using GachaMoon.Domain.Npcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Npcs;
public class NpcCharacterConfiguration : IEntityTypeConfiguration<NpcCharacter>
{
    public void Configure(EntityTypeBuilder<NpcCharacter> builder)
    {
        builder.Property(n => n.Name).IsRequired();
        builder.Property(n => n.NpcType).IsRequired();
        builder.Property(n => n.ChallengeRating).IsRequired();
    }
}