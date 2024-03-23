namespace GachaMoon.WebApi.Endpoints.Models;

public static class SingleObjectResponse
{
    public static SingleObjectResponse<TData> FromData<TData>(TData data)
    {
        return new(data);
    }
}

public record SingleObjectResponse<T>
{
    public SingleObjectResponse(T data)
    {
        Data = data;
    }

    public T Data { get; }
}
