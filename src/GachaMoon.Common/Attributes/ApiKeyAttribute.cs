using GachaMoon.Common.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter, IFilterMetadata
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var apiKey = context.HttpContext.RequestServices.GetRequiredService<ApiKeyOptions>().ApiKey;
        var key = "X-Api-Key";
        if (!context.HttpContext.Request.Headers.TryGetValue(key, out var value) || value.Count == 0 || !apiKey.Equals(value[0], StringComparison.Ordinal))
        {
            throw new NotImplementedException();
        }

        await next();
    }
}