using GachaMoon.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations;

public abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ApplyConfiguration(builder);
    }

    protected virtual void ApplyConfiguration(EntityTypeBuilder<TEntity> builder)
    {
    }
}

public abstract class ConfigurationBase<TEntity, TKey> : ConfigurationBase<TEntity>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    protected override void ApplyConfiguration(EntityTypeBuilder<TEntity> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.HasKey(e => e.Id);
    }
}
