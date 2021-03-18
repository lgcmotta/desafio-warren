using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DesafioWarren.Application.Models;
using DesafioWarren.Domain.Repositories;

namespace DesafioWarren.Application.Queries
{
    public interface IAccountsQueryWrapper
    {
        Task<Response> GetContactsAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<Response> GetAccountTransactions(Guid accountId, CancellationToken cancellationToken = default);
    }

    public class AccountsQueryWrapper : IAccountsQueryWrapper
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        public AccountsQueryWrapper(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetContactsAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var accounts = await _accountRepository.GetAccountsExceptAsync(accountId, cancellationToken);

            var accountsModels = accounts.Select(account => new AccountModelBase
            {
                Name = account.Name
                , Cpf = account.Cpf
                , Email = account.Email
                , PhoneNumber = account.PhoneNumber
                , Currency = account.GetCurrencyIsoCode()
            });

            return new Response(accountsModels);
        }

        public async Task<Response> GetAccountTransactions(Guid accountId, CancellationToken cancellationToken = default)
        {
            var transactions = await _accountRepository.GetAccountTransactionsAsync(accountId, cancellationToken);

            var transactionsModels = _mapper.Map<IEnumerable<AccountTransactionModel>>(transactions);

            return new Response(transactionsModels);
        }
    }
}