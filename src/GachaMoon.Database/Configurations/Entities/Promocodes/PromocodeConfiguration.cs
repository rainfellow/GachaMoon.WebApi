using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Promocodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Promocodes;
public class PromocodeConfiguration : IEntityTypeConfiguration<Promocode>
{
    public void Configure(EntityTypeBuilder<Promocode> builder)
    {
        _ = builder.Property(p => p.Code).IsRequired();
        _ = builder.HasIndex(p => p.Code).WhereNotDeleted().IsUnique();

        _ = builder.HasData([
            new() {
                Id = 1,
                Code = "APRILFOOLS",
                ExpiryDate = new DateOnly(year:2025, month:4, day:1),
                UsesLeft = 100
            },
            new() {
                Id = 2,
                Code = "SORRYFROZEN",
                ExpiryDate = new DateOnly(year:2025, month:4, day:1),
                UsesLeft = 100
            },
            new() {
                Id = 3,
                Code = "GIVEMEROLLS",
                ExpiryDate = new DateOnly(year:2025, month:4, day:1),
                UsesLeft = 100
            }
        ]);
    }
};