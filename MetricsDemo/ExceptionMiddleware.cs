using System;
using System.Net;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MetricsDemo
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetrics _metrics;

        public ExceptionMiddleware(RequestDelegate next, IMetrics metrics)
        {
            _metrics = metrics;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _metrics.Measure.Meter.Mark(MetricsRegistry.Exceptions, new MetricTags("exception_type", ex.Message ?? "Unknown"));
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Booom!"
            }.ToString());
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}