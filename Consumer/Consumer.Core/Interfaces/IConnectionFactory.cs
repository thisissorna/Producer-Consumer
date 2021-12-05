using System.Data;

namespace Consumer.Core.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}
