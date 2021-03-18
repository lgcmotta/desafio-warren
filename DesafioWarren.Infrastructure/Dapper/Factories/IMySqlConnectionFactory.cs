using MySqlConnector;

namespace DesafioWarren.Infrastructure.Dapper.Factories
{
    public interface IMySqlConnectionFactory
    {
        MySqlConnection CreateConnection();
    }
}