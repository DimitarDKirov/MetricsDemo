using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MetricsDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMetrics();
            services.AddSwaggerGen(n =>
                n.SwaggerDoc("v1", new OpenApiInfo {Title = "MetricsDemo", Version = "v1"}));
            services.AddHostedService<ProcessMemory>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(n =>
                n.SwaggerEndpoint("v1/swagger.json", "Metrics Demo V1"));
            app.UseRouting();
            app.ConfigureExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
