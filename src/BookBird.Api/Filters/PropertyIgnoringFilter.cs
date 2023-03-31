using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookBird.Api.Filters
{
    public class PropertyIgnoringFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo
                .GetParameters()
                .SelectMany(p => p.ParameterType
                    .GetProperties()
                    .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() is not null))
                .ToArray();

            if (!ignoredProperties.Any())
            {
                return;
            }

            foreach (var property in ignoredProperties)
            {
                operation.Parameters = operation.Parameters
                    .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture) && !p.In.Equals("route"))
                    .ToList();
            }
        }
    }
}