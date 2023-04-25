using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class ApplicationCorsConfiguration
    {
        public const string AllowAll = nameof(AllowAll);

        public static void AddApplicationCors(this IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyOrigin();

            services.AddCors(opt =>
            {
                opt.AddPolicy(AllowAll, corsBuilder.Build());
            });
        }
    }
}
