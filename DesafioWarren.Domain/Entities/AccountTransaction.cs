using System;
using System.Collections.Generic;
using System.Linq;
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

    internal sealed class SignedTransactionValueFactory
    {
        private static readonly IEnumerable<(int Multiplier, TransactionType TransactionType)> TransactionTypeMultipliers = new[]
        {
            (1, TransactionType.Deposit)
            , (-1, TransactionType.Payment)     
            , (-1, TransactionType.Transfer)
            , (-1, TransactionType.Withdraw)
        };  


        public static decimal GetTransactionValueWithSignal(decimal value, TransactionType transactionType)
        {
            if (value == 0) return value;

            var multiplier = TransactionTypeMultipliers.FirstOrDefault(signalType => signalType.TransactionType == transactionType).Multiplier;

            return value * multiplier;
        }
    }
}