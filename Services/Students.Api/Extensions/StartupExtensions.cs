using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Students.Api.Configurations;

namespace Students.Api.Extensions
{
    internal static class StartupExtensions
    {
        internal static ServiceProvider AddCustomOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ApiConfiguration>(configuration.GetSection(nameof(ApiConfiguration)));

            return services.BuildServiceProvider();
        }
    }
}
