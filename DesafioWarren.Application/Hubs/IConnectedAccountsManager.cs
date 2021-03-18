using System;
using System.Threading.Tasks;

namespace DesafioWarren.Application.Hubs
{
    public interface IConnectedAccountsManager
    {
        void AppendAccountId(Guid accountId, string connectionId);

        void RemoveAccountId(string connectionId);

        string GetAccountConnectedId(Guid accountId);
    }
}