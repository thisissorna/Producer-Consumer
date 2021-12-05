using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;

namespace Consumer.Infrastructure.Redis
{
    public static class RedisSetup
    {
        public static void AddRedis<T>(this IServiceCollection services, RedisConfiguration redisConfiguration) where T : class, ISerializer, new()
        {
            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
            services.AddSingleton<ISerializer, T>();
            services.AddSingleton((provider) =>
            {
                return provider.GetRequiredService<IRedisCacheClient>().GetDbFromConfiguration();
            });
            services.AddSingleton(redisConfiguration);
        }
    }
}
