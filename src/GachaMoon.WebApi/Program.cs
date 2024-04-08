using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using GachaMoon.Application;
using GachaMoon.Common.Extensions;
using GachaMoon.Common.Options;
using GachaMoon.Configurations;
using GachaMoon.Database;
using GachaMoon.MediatR.Common;
using GachaMoon.Services.Abstractions.Database;
using GachaMoon.Services.Time;
using GachaMoon.Utilities.Constants;
using GachaMoon.Utilities.Jwt;
using GachaMoon.WebApi.Common;
using GachaMoon.WebApi.Middlewares;
using GachaMoon.WebApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using NodaTime.Serialization.SystemTextJson;
using NodaTime;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Anime;
using GachaMoon.Clients.Anime;
using GachaMoon.Clients.AnimeList;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.ApplyHostedAppConfiguration(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            _ = builder
                .SetIsOriginAllowed(origin => new Uri(origin).IsLoopback || origin.Contains("http://web.gachamoon.ru", StringComparison.InvariantCultureIgnoreCase))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        _ = opts.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    });

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = false;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.Conventions.Add(new RoutePrefixConvention());
});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    _ = loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDatabaseInitialization();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(), typeof(GachaMoon.Application.Injection).Assembly));
builder.Services.AddCommonBehaviors();

builder.Services.AddApplicationValidators();
builder.Services.AddAndValidateOptions<AppHostOptions>(builder.Configuration, "Host");
builder.Services.AddAndValidateOptions<ApiKeyOptions>(builder.Configuration, "ApiKeyOptions");

builder.Services.AddSystemClockProvider();

builder.Services.AddAnimeClients();
builder.Services.AddUserAnimeListClients();
builder.Services.AddSingleton<IAnimeScreenshotQuizService, JsonAnimeScreenshotQuizService>();

builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("BodyCaching", builder =>
    {
        _ = builder.VaryByValue(
            (context) =>
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = reader.ReadToEndAsync();

                context.Request.Body.Position = 0;

                var keyVal = new KeyValuePair<string, string>("requestBody", body.Result);

                return keyVal;
            }
        );
    });
    options.SizeLimit = 32 * 1024 * 1024;
    options.MaximumBodySize = 512 * 1024;
});

AddJwtAuthentication(builder.Services, builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ApiPolicies.AdminOnlyPolicy, policy =>
                      policy.RequireClaim(UserClaims.IsAdminClaim, "True"));
});

AddSwagger(builder.Services);

var app = builder.Build();

InitializeDb(app);
// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseExceptionHandler(new ExceptionHandlerOptions()
{
    AllowStatusCode404Response = true,
    ExceptionHandlingPath = "/error"
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseResponseCaching();
app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    _ = services.AddAndValidateOptions<JwtOptions>(configuration, "Jwt");

    var jwtConfig = configuration.GetRequiredSection("Jwt");
    _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Audience = jwtConfig["Audience"];
            options.ClaimsIssuer = jwtConfig["Issuer"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["Key"]!)),
                ValidateIssuer = true,
                ValidIssuer = jwtConfig["Issuer"],
                ValidateAudience = true,
            };
        });
}

static void InitializeDb(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    databaseInitializer.Initialize();
}

static void AddSwagger(IServiceCollection services)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    _ = services.AddEndpointsApiExplorer();
    _ = services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });
    _ = services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.OperationFilter<SwaggerApiKeyFilter>();
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
        });
    });
}

public partial class Program { }
