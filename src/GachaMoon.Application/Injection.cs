global using MediatR;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Application;

public static class Injection
{
    public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
