using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DesafioWarren.Application.Hubs
{
    public class AccountsHub : Hub
    {
        private readonly IConnectedAccountsManager _connectedAccountsManager;

        public AccountsHub(IConnectedAccountsManager connectedAccountsManager)
        {
            _connectedAccountsManager = connectedAccountsManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectedAccountsManager.RemoveAccountId(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
        
        public Task AppendAccountToList(Guid accountId, string connectionId)
        {
            _connectedAccountsManager.AppendAccountId(accountId, Context.ConnectionId);

            return Task.CompletedTask;
        }
    }
}