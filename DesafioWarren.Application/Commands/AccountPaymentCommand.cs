using System;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Application.Commands
{
    public class AccountPaymentCommand : FinancialOperationCommand
    {
        public AccountPaymentCommand(Guid accountId, decimal value) : base(accountId, value)
        {
        }

        public override TransactionType GetTransactionType() => TransactionType.Payment;
    }
}