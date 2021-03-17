using System;
using DesafioWarren.Application.Models;

namespace DesafioWarren.Application.Commands
{
    public class UpdateAccountCommand : AccountIdCommand
    {
        public AccountModelBase Account { get; }

        public UpdateAccountCommand(Guid accountId, AccountModelBase account) : base(accountId) 
        {
            Account = account;
        }
    }
}