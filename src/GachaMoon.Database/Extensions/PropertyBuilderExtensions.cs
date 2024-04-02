using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace GachaMoon.Database.Extensions;

public static class PropertyBuilderExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = NodaTime.Serialization.SystemTextJson.Extensions
        .ConfigureForNodaTime(new(JsonSerializerDefaults.General), DateTimeZoneProviders.Tzdb);

    public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class?, new()
    {
        ValueConverter<T, string> converter = new
        (
            v => JsonSerializer.Serialize(v, JsonSerializerOptions),
            v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions) ?? new T()
        );

        ValueComparer<T> comparer = new
        (
            (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions) == JsonSerializer.Serialize(r, JsonSerializerOptions),
            v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions).GetHashCode(StringComparison.Ordinal),
            v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions), JsonSerializerOptions)!
        );

        _ = propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }
}
