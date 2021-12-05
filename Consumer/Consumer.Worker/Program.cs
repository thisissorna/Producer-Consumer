using Consumer.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Consumer.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .UseSerilog((context, loggerConfig) =>
                {
                    loggerConfig
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddInfrastructure(hostContext);
                    services.AddHostedService<Worker>();
                });
    }
}
