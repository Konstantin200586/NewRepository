using SMBTools.Web.DAL.Repositories.Interfaces;
using SMBTools.Web.DAL.Repositories;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class DataAccessDependencyInjection
    {
        public static void ConfigureInternalRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
