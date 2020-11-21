using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Managers;

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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Student group", Version = "v1.0" });                                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/v1/swagger.json", "Student group");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
