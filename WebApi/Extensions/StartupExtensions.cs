using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentGroup.Services.WebApi.Configurations;

namespace StudentGroup.Services.WebApi.Extensions
{
    internal static class StartupExtensions
    {
        internal static ServiceProvider AddCustomOptions(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(ApiConfiguration));
            return services
                .Configure<ApiConfiguration>(configurationSection)
                .BuildServiceProvider();
        }
    }
}
