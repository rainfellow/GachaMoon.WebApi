using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using GachaMoon.Common.Options;

namespace GachaMoon.WebApi.Swagger;
public class SwaggerApiKeyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var isInternalApi = context.ApiDescription.RelativePath?.Contains("internal-api", StringComparison.Ordinal);

        if (isInternalApi is not null and false)
        {
            return;
        }

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ApiKeyOptions.ApiKeyHeader,
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String"
            }
        });
    }
}