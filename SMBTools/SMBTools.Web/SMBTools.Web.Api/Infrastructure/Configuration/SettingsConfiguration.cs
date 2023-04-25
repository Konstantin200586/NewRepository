using SMBTools.Web.Api.Constants;
using SMBTools.Web.BLL.Constants;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Settings;

namespace SMBTools.Web.Api.Infrastructure.Configuration
{
    public static class SettingsConfiguration
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(Options.JwtSettingsSection));
            services.Configure<KeyEndpointPairSetting>(configuration.GetSection(Options.FormRecognizerSettingsSection));
            services.Configure<BlobStorageSettings>(configuration.GetSection(Options.BlobStorageSettingsSection));
        }
    }
}
