using Microsoft.OpenApi.Models;
using SMBTools.Web.BLL.Constants;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class SwaggerConfiguration
    {
        private const string Title = "Prime Baseball internal API";
        private const string Version = "v1";
        private const string AuthMethod = "Bearer";

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo { Title = Title, Version = Version });

                c.AddSecurityDefinition(AuthMethod, new OpenApiSecurityScheme()
                {
                    Name = JwtClaimContextItemConstants.AuthorizationHeaderName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtClaimContextItemConstants.BearerName,
                    BearerFormat = JwtClaimContextItemConstants.TokenAuthenticationType,
                    In = ParameterLocation.Header,
                    Description = string.Empty,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtClaimContextItemConstants.BearerName
                        }
                    },

                    Array.Empty<string>()
                }
            });

            });
        }
    }
}
