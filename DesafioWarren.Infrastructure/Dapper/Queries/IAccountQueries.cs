using System;
using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Domain.Aggregates;

namespace DesafioWarren.Infrastructure.Dapper.Queries
{
    public interface IAccountQueries
    {
        Task<Account> GetAccountById(Guid accountId, CancellationToken cancellationToken = default);

        Task<Account> GetAccountByName(string name, CancellationToken cancellationToken = default);
        
        Task<Account> GetAccountByCpf(string cpf, CancellationToken cancellationToken = default);
        
        Task<Account> GetAccountByNumber(string destinationAccountNumber, CancellationToken cancellationToken = default);
    }
}
