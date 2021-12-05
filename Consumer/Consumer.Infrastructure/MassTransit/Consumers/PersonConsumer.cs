using Consumer.Core.Entities;
using Consumer.Core.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading.Tasks;

namespace Consumer.Infrastructure.MassTransit.Consumers
{
    public class PersonConsumer : IConsumer<Person>
    {
        private readonly ILogger<PersonConsumer> _logger;
        private readonly IRedisCacheClient _redisClient;
        private readonly IPersonRepository _repository;

        public PersonConsumer(ILogger<PersonConsumer> logger,
            IRedisCacheClient redisClient,
            IPersonRepository repository)
        {
            _logger = logger;
            _redisClient = redisClient;
            _repository = repository;
        }

        public Task Consume(ConsumeContext<Person> context)
        {
            _logger.LogInformation("{service} running at {time}", nameof(PersonConsumer), DateTimeOffset.Now);

            try
            {
                var person = context.Message;
                person.Id = _repository.Create(person);
                _redisClient.GetDbFromConfiguration().AddAsync(person.Id.ToString(), person);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{nameof(PersonConsumer)}.{nameof(Consume)} threw an exception.");
            }

            return Task.CompletedTask;
        }
    }
}
