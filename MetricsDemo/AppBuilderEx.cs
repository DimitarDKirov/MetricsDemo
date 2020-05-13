using Microsoft.AspNetCore.Builder;

namespace MetricsDemo
{
    public static class AppBuilderEx
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
