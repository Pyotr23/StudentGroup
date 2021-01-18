using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Api.Configurations;

namespace School.Api.Extensions
{
    internal static class StartupExtensions
    {
        internal static ServiceProvider AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var apiConfigurationSectionName = configuration.GetSection(nameof(ApiConfiguration));
            return services
                .Configure<ApiConfiguration>(apiConfigurationSectionName)
                .BuildServiceProvider();
        }
    }
}
