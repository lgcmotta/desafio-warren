using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioWarren.Application.Hubs
{
    public interface IConnectedAccountsManager
    {
        void AppendAccountId(Guid accountId, string connectionId);

        void RemoveAccountId(string connectionId);

        string GetAccountConnectedId(Guid accountId);
    }

    public class ConnectedAccountsManager : IConnectedAccountsManager
    {
        private readonly List<(Guid AccountId, string ConnectionId)> _connectedAccounts = new();

        public void AppendAccountId(Guid accountId, string connectionId)
        {
            _connectedAccounts.Add((accountId, connectionId));
        }

        public void RemoveAccountId(string connectionId)
        {
            var accountToRemove = _connectedAccounts.FirstOrDefault(connectedAccount =>
                connectedAccount.ConnectionId == connectionId);

            _connectedAccounts.Remove(accountToRemove);
        }

        public string GetAccountConnectedId(Guid accountId)
        {
            var connectedAccount = _connectedAccounts.FirstOrDefault(connectedAccount =>
                connectedAccount.AccountId == accountId);

            return connectedAccount.ConnectionId;
        }
    }
}