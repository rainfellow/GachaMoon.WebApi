using GachaMoon.Services.Abstractions.Clients;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Clients.Shikimori;

public static class Injection
{
    public static IServiceCollection AddShikimoriClients(this IServiceCollection services)
    {
        services.AddKeyedScoped<IGraphQLClient>("ShikimoriGraphql", (s, k) => new GraphQLHttpClient("https://shikimori.one/api/graphql", new NewtonsoftJsonSerializer()));
        services.AddScoped<ShikimoriGraphqlClient>();
        services.AddScoped<IShikimoriClient>(sp => sp.GetRequiredService<ShikimoriGraphqlClient>());
        return services;
    }
}
