using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DesafioWarren.Application.Models;
using DesafioWarren.Application.Services.Identity;
using DesafioWarren.Domain.Repositories;

namespace DesafioWarren.Application.Queries
{
    public interface IAccountsQueryWrapper
    {
        Task<Response> GetContactsAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<Response> GetAccountTransactions(Guid accountId, CancellationToken cancellationToken = default);
        Task<Response> GetMyselfAsync();
    }

    public class AccountsQueryWrapper : IAccountsQueryWrapper
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        private readonly IIdentityService _identityService;

        public AccountsQueryWrapper(IAccountRepository accountRepository, IMapper mapper, IIdentityService identityService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _identityService = identityService;
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

        public async Task<Response> GetMyselfAsync()
        {
            var name = _identityService.GetUserDisplayName();

            var account = await _accountRepository.GetAccountByNameAsync(name);

            return new Response(_mapper.Map<AccountModel>(account));
        }
    }
}