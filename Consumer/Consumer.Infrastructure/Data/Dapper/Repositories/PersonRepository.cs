using AutoMapper;
using Consumer.Core.Entities;
using Consumer.Core.Interfaces;
using Dapper.Contrib.Extensions;
using System.Data;

namespace Consumer.Infrastructure.Data.Dapper.Repositories
{
    class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public PersonRepository(IConnectionFactory connectionFactory, IMapper mapper)
        {
            _connection = connectionFactory.Connection;
            _mapper = mapper;
        }

        public long Create(Person person)
        {
            var p = _mapper.Map<Person, Entities.Person>(person);
            return _connection.Insert(p);
        }
    }
}
