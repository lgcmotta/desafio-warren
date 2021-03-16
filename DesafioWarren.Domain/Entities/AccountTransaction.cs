using System;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Domain.Entities
{
    public sealed class AccountTransaction
    {
        public TransactionType TransactionType { get; }

        public DateTime Occurrence { get; }
        
        public AccountTransaction(TransactionType transactionType, DateTime occurrence)
        {
            TransactionType = transactionType;
            Occurrence = occurrence;
        }

        private AccountTransaction() { }
    }
}