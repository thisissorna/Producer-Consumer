using Bogus;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Producer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                var faker = new Faker("fa");
                await _bus.Publish(new Consumer.Core.Entities.Person
                {
                    Id = 0,
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Age = faker.Random.Number(1, 100)
                }, stoppingToken);
                _logger.LogInformation("New person published at {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
