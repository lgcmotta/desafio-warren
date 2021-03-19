using System.Linq;
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
            var connectedAccounts = _connectedAccountsManager.GetConnectionIdsForAccount(notification.Account.Id).ToList();
            
            await _hubContext.Clients.Clients(connectedAccounts).SendCoreAsync("AccountBalanceChanged", new[] { notification.Account.GetBalance() }, cancellationToken);

        }
    }
}