using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Managers;
using StudentGroup.Services.Api.Configurations;
using StudentGroup.Services.Api.Extensions;
using System.IO;

namespace StudentGroup.Services.Api
{
    /// <summary>
    ///     Настройка веб-приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiConfiguration = GetApiConfiguration(services);
            services
                .AddSwaggerGen(options =>
                    options.SwaggerDoc(apiConfiguration.Version, (OpenApiInfo)apiConfiguration))
                .ConfigureSwaggerGen(options => 
                {
                    options.CustomSchemaIds(x => x.FullName);
                    var basePath = Directory.GetCurrentDirectory();
                    var xmlFileName = typeof(Startup).Namespace;
                    var xmlPath = Path.Combine(basePath, $"{xmlFileName}.xml");
                    options.IncludeXmlComments(xmlPath);
                });

            var connectionString = Configuration.GetConnectionString("Default");
            services
                .AddDbContext<SchoolContext>(opt => opt.UseSqlServer(connectionString))
                .AddTransient<ISchoolManager, SchoolManager>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
