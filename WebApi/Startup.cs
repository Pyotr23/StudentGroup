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
using StudentGroup.Services.WebApi.Configurations;
using StudentGroup.Services.WebApi.Extensions;

namespace StudentGroup.Services.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            services
                .AddDbContext<SchoolContext>(opt => opt.UseSqlServer(connectionString))
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddTransient<ISchoolManager, SchoolManager>()
                .AddControllers();

            var apiConfiguration = services
                .AddCustomOptions(Configuration)
                .GetRequiredService<IOptions<ApiConfiguration>>()
                .Value;
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(apiConfiguration.Version, 
                    new OpenApiInfo { Title = apiConfiguration.Name, Version = apiConfiguration.Version });                                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ApiConfiguration> apiOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            var apiConfiguration = apiOptions.Value;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", apiConfiguration.Name);
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
