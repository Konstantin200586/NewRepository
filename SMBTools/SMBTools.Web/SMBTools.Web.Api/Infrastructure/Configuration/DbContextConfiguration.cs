using Microsoft.EntityFrameworkCore;
using SMBTools.Web.DAL.Infrastructure;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class DbContextConfiguration
    {
        private const string ConnectionStringName = "MySQL";
        private const int MajorVersion = 8;
        private const int MinorVersion = 0;
        private const int BuildVersion = 28;

        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(MajorVersion, MinorVersion, BuildVersion))));
        }
    }
}
