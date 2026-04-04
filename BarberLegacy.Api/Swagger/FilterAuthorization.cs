using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BarberLegacy.Api.Swagger
{
    public class FilterAuthorization : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
 
            if (!context.ApiDescription.ActionDescriptor
                .EndpointMetadata.OfType<AuthorizeAttribute>().Any())
            {
                return;
            }

            if (context.ApiDescription.ActionDescriptor
                .EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            // config del esquema bearer cuando sea necesario
            operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
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
                    new string[] { }
                }
            }
        };
        }
    }
}
