using Consumer.Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Consumer.Infrastructure.Data
{
    class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;

        public IDbConnection Connection
        {
            get
            {
                IDbConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                return conn;
            }
        }

        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
    }
}
