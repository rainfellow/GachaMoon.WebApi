using GachaMoon.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Extensions;

public static class IndexBuilderExtensions
{
    public static IndexBuilder WhereNotDeleted(this IndexBuilder builder)
    {
        return builder.HasFilter($"\"{nameof(ISoftDeleteEntity.DeletedAt)}\" is NULL");
    }
}
