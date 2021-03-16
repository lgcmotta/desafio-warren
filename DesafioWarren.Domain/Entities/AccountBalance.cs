using System;
using System.Collections.Generic;
using System.Linq;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Domain.Entities
{
    public sealed class AccountBalance : Entity
    {
        private decimal _balance;

        private ICollection<AccountTransaction> _transactions = new List<AccountTransaction>();

        public IEnumerable<AccountTransaction> Transactions => _transactions;

        public decimal Balance => _balance;

        public AccountBalance()
        {
            _balance = 0;
        }
        
        private void AddTransaction(TransactionType transactionType) =>
            _transactions.Add(new AccountTransaction(transactionType, DateTime.Now));

        public void Deposit(decimal value)
        {
            var backupBalance = _balance;

            try
            {
                _balance += value;

                AddTransaction(TransactionType.Deposit);
            }
            catch
            {
                _balance = backupBalance;
            }
        }

        public void Transfer(Account destination, decimal value)
        {
            var backupBalance = _balance;

            try
            {
                _balance -= value;

                destination.Deposit(value);

                AddTransaction(TransactionType.Transfer);
            }
            catch
            {
                _balance = backupBalance;
            }
        }

        public void Payment(decimal value)
        {
            var backupBalance = _balance;

            try
            {
                _balance -= value;

                AddTransaction(TransactionType.Payment);
            }
            catch
            {
                _balance = backupBalance;
            }
        }

        public decimal Withdraw(decimal value)
        {
            var backupBalance = _balance;

            try
            {
                _balance -= value;

                AddTransaction(TransactionType.Withdraw);

                return _balance;
            }
            catch
            {
                _balance = backupBalance;

                return 0;
            }
        }

        
    }
}