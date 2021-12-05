using Consumer.Core.Interfaces;
using Consumer.Infrastructure.Data;
using Consumer.Infrastructure.Data.Dapper.Profiles;
using Consumer.Infrastructure.Data.Dapper.Repositories;
using Consumer.Infrastructure.MassTransit.Consumers;
using Consumer.Infrastructure.Redis;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Consumer.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, HostBuilderContext hostContext)
        {
            var config = hostContext.Configuration;
            services.AddAutoMapper(typeof(PersonProfile));
            services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddRedis<SystemTextJsonSerializer>(config.GetSection("Redis").Get<RedisConfiguration>());
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = config.GetSection("MassTransit:RabbitMQ");
                    cfg.Host(rabbitConfig.GetValue<string>("Host"),
                        rabbitConfig.GetValue<ushort>("Port"),
                        rabbitConfig.GetValue<string>("VirtualHost"),
                        configurator =>
                        {
                            configurator.Username(rabbitConfig.GetValue<string>("Username"));
                            configurator.Password(rabbitConfig.GetValue<string>("Password"));
                        });
                    cfg.ConfigureEndpoints(context);
                });
                x.AddConsumer<PersonConsumer>();
            });
            services.AddMassTransitHostedService(true);
        }
    }
}
