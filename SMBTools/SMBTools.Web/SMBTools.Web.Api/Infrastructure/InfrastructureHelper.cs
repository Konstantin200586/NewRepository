using FluentValidation.AspNetCore;
using SMBTools.Web.Api.Constants;
using SMBTools.Web.Api.Infrastructure.Configuration;
using SMBTools.Web.Api.Mapper;
using SMBTools.Web.Api.Middleware;
using SMBTools.Web.BLL.Mapper;
using SMBTools.Web.BLL.Settings;
using SMBTools.Web.DAL.Mapping;
using System.Reflection;

namespace SMBTools.Web.Api.Infrastructure
{
    public static class InfrastructureHelper
    {
        private const string ResourcesPath = "Resources";

        public static void InitServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            var jwtSettingsChildren = configuration.GetSection(Options.JwtSettingsSection);
            _ = int.TryParse(jwtSettingsChildren[nameof(JwtSettings.ExpirationMinutes)], out int expirationMinutes);
            _ = int.TryParse(jwtSettingsChildren[nameof(JwtSettings.RefreshTokenExpirationMinutes)], out int refreshTokenExpirationMinutes);

            var jwtSettings = new JwtSettings();
            jwtSettings.ExpirationMinutes = expirationMinutes;
            jwtSettings.Audience = jwtSettingsChildren[nameof(JwtSettings.Audience)];
            jwtSettings.Issuer = jwtSettingsChildren[nameof(JwtSettings.Issuer)];
            jwtSettings.SymmetricSecurityKey = jwtSettingsChildren[nameof(JwtSettings.SymmetricSecurityKey)];
            jwtSettings.RefreshTokenExpirationMinutes = refreshTokenExpirationMinutes;

            services.ConfigureInternalServices();
            services.ConfigureInternalRepository();
            services.AddJsonSerializerOptions();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddLocalization(options => options.ResourcesPath = ResourcesPath);

            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)),
                Assembly.GetAssembly(typeof(BllMappingProfile)),
                Assembly.GetAssembly(typeof(AccountMapperProfile)));

            services.AddJwtAuthentication(jwtSettings);
            services.AddSettings(configuration);

            services.InitDbContext(configuration);
            services.ConfigureSwagger();
            services.AddFluentValidationAutoValidation();
            services.AddHttpContextAccessor();
            services.AddApplicationCors();
        }

        public static void InitApp(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseCors(ApplicationCorsConfiguration.AllowAll);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
