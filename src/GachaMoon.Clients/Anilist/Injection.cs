using GachaMoon.Services.Abstractions.Clients;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Clients.Anilist;

public static class Injection
{
    public static IServiceCollection AddAnilistClients(this IServiceCollection services)
    {
        services.AddKeyedScoped<IGraphQLClient>("AnilistGraphql", (s, k) => new GraphQLHttpClient("https://graphql.anilist.co", new NewtonsoftJsonSerializer()));
        services.AddScoped<AnilistGraphqlClient>();
        services.AddScoped<IAnilistClient>(sp => sp.GetRequiredService<AnilistGraphqlClient>());
        return services;
    }
}
