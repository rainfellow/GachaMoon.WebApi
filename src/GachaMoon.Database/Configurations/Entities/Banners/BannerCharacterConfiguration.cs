// src/GachaMoon.Database/Configurations/Entities/Banners/BannerCharacterConfiguration.cs
using GachaMoon.Domain.Banners;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Banners;
public class BannerCharacterConfiguration : ConfigurationBase<BannerCharacter>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<BannerCharacter> builder)
    {
        base.ApplyConfiguration(builder);
        builder.Property(x => x.BannerId).IsRequired();
        builder.Property(x => x.CharacterId).IsRequired();

        // Add foreign key relationship to Banner using BannerId
        builder.HasOne(x => x.Banner)
            .WithMany(x => x.BannerCharacters)
            .HasForeignKey(x => x.BannerId);

        // Add foreign key relationship to Character using CharacterId
        builder.HasOne(x => x.Character)
            .WithMany()
            .HasForeignKey(x => x.CharacterId);
    }
}