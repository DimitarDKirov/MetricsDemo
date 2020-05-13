using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MetricsDemo
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5000")
                .UseStartup<Startup>()
                .Build();
        }

        public static void Main(string[] args) { BuildWebHost(args).Run(); }
    }
}
