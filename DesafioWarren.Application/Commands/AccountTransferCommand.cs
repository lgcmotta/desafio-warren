using System;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Application.Commands
{
    public class AccountTransferCommand : FinancialOperationCommand
    {
        public AccountTransferCommand(Guid accountId, decimal value) : base(accountId, value)
        {
        }

        public override TransactionType GetTransactionType() => TransactionType.Transfer;
    }
}