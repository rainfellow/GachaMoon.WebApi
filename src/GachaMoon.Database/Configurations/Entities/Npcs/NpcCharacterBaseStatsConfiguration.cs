using GachaMoon.Domain.Npcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Npcs;
public class NpcCharacterBaseStatsConfiguration : IEntityTypeConfiguration<NpcCharacterBaseStats>
{
    public void Configure(EntityTypeBuilder<NpcCharacterBaseStats> builder)
    {
        builder.Property(n => n.NpcCharacterId).IsRequired();
        builder.HasOne(n => n.NpcCharacter)
            .WithOne()
            .HasForeignKey<NpcCharacterBaseStats>(n => n.NpcCharacterId);
        builder.Property(n => n.Attack).IsRequired();
        builder.Property(n => n.Defence).IsRequired();
        builder.Property(n => n.Health).IsRequired();
    }
}