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
            return services
                .Configure<ApiConfiguration>(configuration.GetSection(nameof(ApiConfiguration)))
                .BuildServiceProvider();
        }
    }
}
