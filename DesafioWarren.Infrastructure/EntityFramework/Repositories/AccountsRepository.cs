using System.Collections.Generic;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Repositories;
using DesafioWarren.Domain.UnitOfWork;
using DesafioWarren.Infrastructure.EntityFramework.DbContexts;

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
    }
}