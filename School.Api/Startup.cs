using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using School.Api.Configurations;
using School.Api.Extensions;
using School.Api.Mapping;
using School.Core;
using School.Core.Services;
using School.Data;
using School.Services;
using School.Services.Mapping;
using System.IO;

namespace School.Api
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
            services                
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(apiConfiguration.Version, (OpenApiInfo)apiConfiguration);
                })
                .ConfigureSwaggerGen(options =>
                {
                    options.CustomSchemaIds(x => x.FullName);
                    var basePath = Directory.GetCurrentDirectory();
                    var xmlFileName = typeof(Startup).Namespace;
                    var xmlPath = Path.Combine(basePath, $"{xmlFileName}.xml");
                    options.IncludeXmlComments(xmlPath);
                });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMappingProfile());
                mc.AddProfile(new ServiceMappingProfile());                 
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services
            //    .AddAutoMapper(typeof(Startup));

            var connectionString = Configuration.GetConnectionString("Default");
            services
                .AddDbContext<SchoolDbContext>(options =>
                    options.UseSqlServer(
                        connectionString,
                        builder => builder.MigrationsAssembly("School.Data"))
                )
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient<IStudentService, StudentService>()
                .AddControllers();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ApiConfiguration> apiOption)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            var apiConfiguration = apiOption.Value;
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiConfiguration.Name);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
