using System.Net.Sockets;
using GachaMoon.Clients.Base;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace GachaMoon.Clients.Anime;

public static class Injection
{
    private static readonly IReadOnlyCollection<Func<HttpRequestMessage, bool>> DefaultAllowRetryFilters =
        new List<Func<HttpRequestMessage, bool>>()
        {
            (r) => r.Method == HttpMethod.Get
        };

    public static IServiceCollection AddAnimeClients(this IServiceCollection services)
    {
        services.AddSingleton<ApiAnimeListClient>();
        services.AddSingleton<IAnimeClient>(sp => sp.GetRequiredService<ApiAnimeListClient>());

        services.AddHttpClient(ExternalClientType.AnimeList.ToString())
            .ConfigureHttpClient((sp, c) =>
            {
                c.BaseAddress = new Uri("https://find-my-anime.dtimur.de/");
            })
            .AddPolicyHandler((sp, request) => AddRetryPolicy(sp, request, DefaultAllowRetryFilters))
            .ConfigurePrimaryHttpMessageHandler(_ => new NoSslHttpClientHandler());
        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> AddRetryPolicy(
        IServiceProvider serviceProvider,
        HttpRequestMessage request,
        IReadOnlyCollection<Func<HttpRequestMessage, bool>>? allowRetryFilters = null)
    {
        if (allowRetryFilters?.Any(f => f(request)) == true)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<SocketException>()
                .OrInner<SocketException>()
                .Or<TimeoutRejectedException>()
                .OrResult(response => (int)response.StatusCode == 429)
                .WaitAndRetryAsync(
                    3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt - 1)),
                    onRetryAsync: (outcome, timespan, retryAttempt, _context) =>
                        LogRetry(serviceProvider, outcome, retryAttempt, timespan));

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60));
            return retryPolicy.WrapAsync(timeoutPolicy);
        }

        return Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
    }

    private static async Task LogRetry(
        IServiceProvider serviceProvider,
        DelegateResult<HttpResponseMessage> outcome,
        int retryAttempt,
        TimeSpan timespan)
    {
        string? response = null;

        try
        {
            var content = outcome?.Result?.Content;
            if (content != null)
            {
                response = await content.ReadAsStringAsync();
            }
        }
        catch { }

        var logger = serviceProvider.GetRequiredService<ILogger<HttpClient>>();

        logger.LogError(outcome?.Exception, "Delaying for {DelayMs}ms, then retrying, attempt: {RetryAttempt}" +
            ". Status code: {StatusCode}. Response: {Response}",
                timespan.TotalMilliseconds, retryAttempt, outcome?.Result?.StatusCode, response);

        outcome?.Result?.Dispose();
    }
}
