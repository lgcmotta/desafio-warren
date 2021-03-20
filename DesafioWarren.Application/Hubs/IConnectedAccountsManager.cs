using System;
using System.Collections.Generic;

namespace DesafioWarren.Application.Hubs
{
    public interface IConnectedAccountsManager
    {
        void AppendAccountId(Guid accountId, string connectionId);

        void RemoveAccountId(string connectionId);

        IEnumerable<string> GetConnectionIdsForAccount(Guid accountId);
    }
}