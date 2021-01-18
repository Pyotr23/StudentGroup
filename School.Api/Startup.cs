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
        private const string SwaggerEndPointUrl = "/swagger/v1/swagger.json";
        private const string ConnectionStringName = "Default";
        private const string MigrationsAssemblyName = "School.Data";

        private IServiceCollection _services;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
                
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;

            SetSwaggerGen();
            SetAutoMapper();
            SetDbContext();

            services                
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient<IGroupsService, GroupsService>()
                .AddTransient<IStudentsService, StudentsService>()                
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
                c.SwaggerEndpoint(SwaggerEndPointUrl, apiConfiguration.Name);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }        

        private void SetSwaggerGen()
        {
            var apiConfiguration = GetApiConfiguration();
            _services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(apiConfiguration.Version, (OpenApiInfo)apiConfiguration);
                })
                .ConfigureSwaggerGen(options =>
                {
                    options.CustomSchemaIds(x => x.FullName);
                    var basePath = Directory.GetCurrentDirectory();
                    var xmlFileName = typeof(Startup).Namespace;
                    var xmlPath = Path.Combine(basePath, xmlFileName + ".xml");
                    options.IncludeXmlComments(xmlPath);
                });
        }

        private ApiConfiguration GetApiConfiguration()
        {
            return _services
                .AddCustomOptions(Configuration)
                .GetRequiredService<IOptions<ApiConfiguration>>()
                .Value;
        }

        private void SetAutoMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMappingProfile());
                mc.AddProfile(new ServiceMappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            _services.AddSingleton(mapper);
        }

        private void SetDbContext()
        {
            var connectionString = Configuration.GetConnectionString(ConnectionStringName);
            _services.AddDbContext<SchoolDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly(MigrationsAssemblyName));
            });
        }
    }
}
