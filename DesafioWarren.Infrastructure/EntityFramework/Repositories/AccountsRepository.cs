using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Repositories;
using DesafioWarren.Domain.UnitOfWork;
using DesafioWarren.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioWarren.Infrastructure.EntityFramework.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
        private readonly AccountsDbContext _context;

        public IUnitOfWork UnitOfWork => _context;
        
        public AccountsRepository(AccountsDbContext context)
        {
            _context = context;
        }

        public void Add(Account account)
        {
            _context.Set<Account>().Add(account);
        }

        public void AddRange(IEnumerable<Account> accounts)
        {
            _context.Set<Account>().AddRange(accounts);
        }

        public void Remove(Account account)
        {
            _context.Set<Account>().Remove(account);
        }

        public void RemoveRange(IEnumerable<Account> accounts)
        {
            _context.Set<Account>().RemoveRange(accounts);
        }

        public async Task<Account> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Account>()
                .Include("_accountBalance")
                .FirstOrDefaultAsync(account => account.Id == accountId, cancellationToken: cancellationToken);
        }

        public async Task<Account> GetAccountByNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Account>()
                .Include("_accountBalance")
                .FirstOrDefaultAsync(account => account.Number == accountNumber, cancellationToken);
        }
    }
}