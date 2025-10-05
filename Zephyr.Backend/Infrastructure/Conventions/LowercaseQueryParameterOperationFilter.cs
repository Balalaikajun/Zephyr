using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zephyr.Backend.Infrastructure.Conventions;

/// <summary>
///     Фильтр операций для приведения параметров запросов Swagger в CamelCase
/// </summary>
public class LowercaseQueryParameterOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) return;

        foreach (var parameter in operation.Parameters)
            if (parameter.In == ParameterLocation.Query)
            {
                var name = parameter.Name;
                if (!string.IsNullOrEmpty(name)) parameter.Name = char.ToLowerInvariant(name[0]) + name.Substring(1);
            }
    }
}