using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Promocodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Promocodes;
public class PromocodeConfiguration : IEntityTypeConfiguration<Promocode>
{
    public void Configure(EntityTypeBuilder<Promocode> builder)
    {
        builder.Property(p => p.Code).IsRequired();
        builder.HasIndex(p => p.Code).WhereNotDeleted().IsUnique();
    }
};