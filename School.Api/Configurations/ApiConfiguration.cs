using Microsoft.OpenApi.Models;

namespace School.Api.Configurations
{
    public class ApiConfiguration
    {
        public string Version { get; set; }
        public string Name { get; set; }

        public static explicit operator OpenApiInfo(ApiConfiguration apiConfiguration)
        {
            return new OpenApiInfo
            {
                Title = apiConfiguration.Name,
                Version = apiConfiguration.Version
            };
        }
    }
}
