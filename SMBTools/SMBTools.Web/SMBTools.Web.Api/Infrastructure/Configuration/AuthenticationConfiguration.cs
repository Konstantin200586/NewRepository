using System.Net.Mime;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SMBTools.Web.Api.Responses;
using SMBTools.Web.BLL.Settings;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = jwtSettings.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse<object>(nameof(AuthenticationException), StatusCodes.Status401Unauthorized)));
                        }
                    };
                });
        }
    }
}
