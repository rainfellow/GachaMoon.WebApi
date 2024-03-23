namespace GachaMoon.WebApi.Endpoints.Models;

public static class CollectionResponse
{
    public static CollectionResponse<TData> FromData<TData>(ICollection<TData> data)
    {
        return new(data);
    }
}

public record CollectionResponse<T>
{
    public CollectionResponse(ICollection<T> data)
    {
        Items = data;
    }

    public ICollection<T> Items { get; }
}
