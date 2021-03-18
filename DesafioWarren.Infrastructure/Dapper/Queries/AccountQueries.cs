using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Infrastructure.Dapper.Factories;

namespace DesafioWarren.Infrastructure.Dapper.Queries
{
    public class AccountQueries : IAccountQueries
    {
        private readonly IMySqlConnectionFactory _connectionFactory;

        public AccountQueries(IMySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Account> GetAccountById(Guid accountId, CancellationToken cancellationToken = default)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            var query = "SELECT * FROM Accounts WHERE Id = @AccountId";

            return await connection.QuerySingleOrDefaultAsync<Account>(query, new {accountId});
        }

        public async Task<Account> GetAccountByName(string name, CancellationToken cancellationToken = default)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            var query = "SELECT * FROM Accounts WHERE Name = @Name";

            return await connection.QuerySingleOrDefaultAsync<Account>(query, new { name });
        }

        public async Task<Account> GetAccountByCpf(string cpf, CancellationToken cancellationToken = default)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            var query = "SELECT * FROM Accounts WHERE Cpf = @Cpf";

            return await connection.QuerySingleOrDefaultAsync<Account>(query, new { cpf });
        }
    }
}