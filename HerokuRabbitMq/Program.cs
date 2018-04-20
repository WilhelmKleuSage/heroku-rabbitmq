using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HerokuRabbitMq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            ConfigurePortBinding(webHost);
            return webHost.Build();
        }

        private static void ConfigurePortBinding(IWebHostBuilder webHost)
        {
            var port = Environment.GetEnvironmentVariable("PORT");

            if (!string.IsNullOrEmpty(port))
            {
                webHost.UseUrls("http://*:" + port);
            }
        }
    }
}
