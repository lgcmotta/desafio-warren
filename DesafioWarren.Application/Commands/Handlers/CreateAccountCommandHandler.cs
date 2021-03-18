using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DesafioWarren.Application.Models;
using DesafioWarren.Application.Services.Identity;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Repositories;
using MediatR;

namespace DesafioWarren.Application.Commands.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response>
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IIdentityService _identityService;

        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, IIdentityService identityService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<Response> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<Account>(request.Account);

            account.DefineCurrency(request.Account.Currency);

            _accountRepository.Add(account);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var accountModel = _mapper.Map<AccountModel>(account);

            var response = new Response(accountModel);

            response.SetResponsePath($"{_identityService.GetRequestPath()}/{account.Id}");

            return response;
        }
    }

    public class AccountDepositCommandHandler : IRequestHandler<AccountDepositCommand, Response>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountDepositCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Response> Handle(AccountDepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);

            account.Deposit(request.Value);

            return new Response($"{request.GetTransactionType().Value}: {account.GetCurrencySymbol()}{request.Value} - OK");
        }
    }
}