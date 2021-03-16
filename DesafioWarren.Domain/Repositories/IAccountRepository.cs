using System.Collections.Generic;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.UnitOfWork;

namespace DesafioWarren.Domain.Repositories
{
    public interface IAccountRepository
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(Account account);

        void AddRange(IEnumerable<Account> accounts);

        void Remove(Account account);

        void RemoveRange(IEnumerable<Account> accounts);

    }
}