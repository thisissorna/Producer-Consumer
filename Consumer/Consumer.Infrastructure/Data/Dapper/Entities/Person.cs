using Dapper.Contrib.Extensions;

namespace Consumer.Infrastructure.Data.Dapper.Entities
{
    [Table("Consumer.Persons")]
    class Person: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
