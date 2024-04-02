using GachaMoon.Domain.Npcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Npcs;
public class NpcCharacterBaseStatsConfiguration : IEntityTypeConfiguration<NpcCharacterBaseStats>
{
    public void Configure(EntityTypeBuilder<NpcCharacterBaseStats> builder)
    {
        _ = builder.Property(n => n.NpcCharacterId).IsRequired();
        _ = builder.HasOne(n => n.NpcCharacter)
            .WithOne()
            .HasForeignKey<NpcCharacterBaseStats>(n => n.NpcCharacterId);
        _ = builder.Property(n => n.Attack).IsRequired();
        _ = builder.Property(n => n.Defence).IsRequired();
        _ = builder.Property(n => n.Health).IsRequired();
    }
}