using SMBTools.Web.BLL.Helpers;
using SMBTools.Web.BLL.Services;
using SMBTools.Web.BLL.Services.Interfaces;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class BusinessLogicDependencyInjection
    {
        public static void ConfigureInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRecognizerService, RecognizerService>();

            services.AddScoped<AuthenticationHelper>();
        }
    }
}
