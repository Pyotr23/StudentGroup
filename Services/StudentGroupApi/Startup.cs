using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Managers;
using StudentGroup.Services.Api.Configurations;
using StudentGroup.Services.Api.Extensions;

namespace StudentGroup.Services.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiConfiguration = GetApiConfiguration(services);
            services.AddSwaggerGen(options =>
                options.SwaggerDoc(apiConfiguration.Version, (OpenApiInfo)apiConfiguration));

            var connectionString = Configuration.GetConnectionString("Default");
            services
                .AddDbContext<SchoolContext>(opt => opt.UseSqlServer(connectionString))
                .AddTransient<ISchoolManager, SchoolManager>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ApiConfiguration> apiOption)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseSwagger();

            var apiConfiguration = apiOption.Value;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", apiConfiguration.Name);
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private ApiConfiguration GetApiConfiguration(IServiceCollection services)
        {
            return services
                .AddCustomOptions(Configuration)
                .GetRequiredService<IOptions<ApiConfiguration>>()
                .Value;
        }
    }
}
