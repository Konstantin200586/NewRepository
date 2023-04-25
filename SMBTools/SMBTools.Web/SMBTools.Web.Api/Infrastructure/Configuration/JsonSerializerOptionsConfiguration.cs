using SMBTools.Contract.CustomConverters;

namespace SMBTools.Web.Api.Infrastructure.Configuration;

public static class JsonSerializerOptionsConfiguration
{
    public static void AddJsonSerializerOptions(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(j =>
        {
            j.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        });
    }
}