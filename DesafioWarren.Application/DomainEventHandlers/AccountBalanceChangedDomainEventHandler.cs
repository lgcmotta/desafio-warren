using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Application.Hubs;
using DesafioWarren.Domain.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace DesafioWarren.Application.DomainEventHandlers
{
    public class AccountBalanceChangedDomainEventHandler : INotificationHandler<AccountBalanceChangedDomainEvent>
    {
        private readonly IHubContext<AccountsHub> _hubContext;

        private readonly IConnectedAccountsManager _connectedAccountsManager;

        public AccountBalanceChangedDomainEventHandler(IHubContext<AccountsHub> hubContext, IConnectedAccountsManager connectedAccountsManager)
        {
            _hubContext = hubContext;
            _connectedAccountsManager = connectedAccountsManager;
        }

        public async Task Handle(AccountBalanceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var connectedAccount = _connectedAccountsManager.GetAccountConnectedId(notification.Account.Id);

            await _hubContext.Clients.Client(connectedAccount).SendCoreAsync("SendMessage", new[] {"foo", "bar"}, cancellationToken);
        }
    }
}