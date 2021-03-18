using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DesafioWarren.Application.Hubs
{
    public class AccountsHub : Hub<IAccountsHub>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendMessage(user, message);
        }

        public async Task AppendAccountToList(Guid accountId)
        {
            Console.WriteLine(accountId);
        }
    }

    public interface IAccountsHub
    {
        public Task SendMessage(string user, string message);
    }
}