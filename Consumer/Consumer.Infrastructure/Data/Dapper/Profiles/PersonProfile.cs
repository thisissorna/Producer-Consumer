using AutoMapper;
using Consumer.Infrastructure.Data.Dapper.Entities;

namespace Consumer.Infrastructure.Data.Dapper.Profiles
{
    class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<Core.Entities.Person, Person>();
            CreateMap<Person, Core.Entities.Person>();
        }
    }
}
