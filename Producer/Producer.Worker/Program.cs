using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Producer.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddMassTransit(cfg =>
                    {
                        cfg.UsingRabbitMq((context, config) =>
                        {
                            var rabbitConfig = hostContext.Configuration.GetSection("MassTransit:RabbitMQ");
                            config.Host(rabbitConfig.GetValue<string>("Host"),
                                rabbitConfig.GetValue<ushort>("Port"),
                                rabbitConfig.GetValue<string>("VirtualHost"),
                                configurator =>
                                {
                                    configurator.Username(rabbitConfig.GetValue<string>("Username"));
                                    configurator.Password(rabbitConfig.GetValue<string>("Password"));
                                });
                            config.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService(true);
                    services.AddHostedService<Worker>();
                });
    }
}
