using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioWarren.Application.Hubs
{
    public class ConnectedAccountsManager : IConnectedAccountsManager
    {
        private readonly List<(Guid AccountId, string ConnectionId)> _connectedAccounts = new();

        public void AppendAccountId(Guid accountId, string connectionId)
        {
            var accounts = _connectedAccounts.Where(tuple => tuple.AccountId == accountId).ToList();

            foreach (var account in accounts)
            {
                _connectedAccounts.Remove(account);
            }

            _connectedAccounts.Add((accountId, connectionId));
        }

        public void RemoveAccountId(string connectionId)
        {
            var accountToRemove = _connectedAccounts.FirstOrDefault(connectedAccount =>
                connectedAccount.ConnectionId == connectionId);

            _connectedAccounts.Remove(accountToRemove);
        }

        public IEnumerable<string> GetConnectionIdsForAccount(Guid accountId)
        {
            return _connectedAccounts.Where(connectedAccount =>
                connectedAccount.AccountId == accountId).Select(connectedAccount => connectedAccount.ConnectionId);
        }
    }
}