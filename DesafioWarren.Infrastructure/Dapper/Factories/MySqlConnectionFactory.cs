using DesafioWarren.Infrastructure.EntityFramework.DbContexts;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace DesafioWarren.Infrastructure.Dapper.Factories
{
    public class MySqlConnectionFactory : IMySqlConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(nameof(AccountsDbContext));
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}