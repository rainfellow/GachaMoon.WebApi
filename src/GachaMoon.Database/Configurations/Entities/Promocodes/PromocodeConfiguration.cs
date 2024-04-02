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
                Code = "APRILFOOLS"
            },
            new() {
                Id = 2,
                Code = "SORRYFROZEN"
            },
            new() {
                Id = 3,
                Code = "GIVEMEROLLS"
            }
        ]);
    }
};