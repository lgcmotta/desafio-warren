using System;
using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Application.Hubs;
using DesafioWarren.Application.Models;
using DesafioWarren.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace DesafioWarren.Application.Commands.Handlers
{
    public class AccountDepositCommandHandler : IRequestHandler<AccountDepositCommand, Response>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHubContext<AccountsHub> _hubContext;

        public AccountDepositCommandHandler(IAccountRepository accountRepository, IHubContext<AccountsHub> hubContext)
        {
            _accountRepository = accountRepository;
            _hubContext = hubContext;
        }

        public async Task<Response> Handle(AccountDepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId, cancellationToken);

            account.Deposit(request.Value);

            var transactionResult = new TransactionResult("OK"
                , DateTime.Now
                , $"{request.GetTransactionType().Value} of {account.GetCurrencySymbol()}{request.Value} was successfully made.");

            await _hubContext.Clients.All.SendCoreAsync("SendMessage", new[] {"foo", "bar"}, cancellationToken);

            return new Response(transactionResult);
        }
    }
}