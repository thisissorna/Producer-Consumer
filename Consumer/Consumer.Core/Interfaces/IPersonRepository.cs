using Consumer.Core.Entities;

namespace Consumer.Core.Interfaces
{
    public interface IPersonRepository
    {
        long Create(Person person);
    }
}
