using System;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Domain.Entities
{
    public sealed class AccountTransaction : Entity
    {
        private decimal _transactionValue;
        
        public TransactionType TransactionType { get; private set; }

        public DateTime Occurrence { get; private set; }

        public decimal TransactionValue
        {
            get => SignedTransactionValueFactory.GetTransactionValueWithSignal(_transactionValue, TransactionType);
            set => throw new NotImplementedException();
        }

        public AccountTransaction(TransactionType transactionType, DateTime occurrence, decimal transactionValue)
        {
            TransactionType = transactionType;
            Occurrence = occurrence;
            _transactionValue = transactionValue;
        }

        private AccountTransaction()
        {
                
        }
        
    }
}