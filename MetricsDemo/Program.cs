using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MetricsDemo
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureMetrics(
                    builder =>
                    {
                        builder.Configuration.Configure(
                            options =>
                            {
                                options.DefaultContextLabel = "Demo";
                                options.ReportingEnabled = true;
                                options.Enabled = true;
                            });
                        builder.Report.ToInfluxDb(
                            options =>
                            {
                                options.InfluxDb.BaseUri = new Uri("http://127.0.0.1:8086");
                                options.InfluxDb.Database = "db_demo";
                                options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                                options.InfluxDb.CreateDataBaseIfNotExists = true;
                            });
                    })
                .UseMetrics()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5000")
                .UseStartup<Startup>()
                .Build();
        }

        public static void Main(string[] args) { BuildWebHost(args).Run(); }
    }
}
