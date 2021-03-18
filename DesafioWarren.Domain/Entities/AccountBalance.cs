using System;
using System.Collections.Generic;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Domain.Entities
{
    public sealed class AccountBalance : Entity
    {
        private decimal _balance;

        private Currency _currency;

        private ICollection<AccountTransaction> _transactions = new List<AccountTransaction>();

        public IEnumerable<AccountTransaction> Transactions => _transactions;

        public Currency Currency => _currency;

        public decimal Balance => _balance;

        public AccountBalance(Currency currency)
        {
            _balance = 0;
            _currency = currency;
        }

        public AccountBalance()
        {
            _balance = 0;
            _currency = Currency.BrazilianReal;
        }
        
        private void AddTransaction(TransactionType transactionType, decimal transactionValue) =>
            _transactions.Add(new AccountTransaction(transactionType, DateTime.Now, transactionValue));

        public void Deposit(decimal value)
        {
            var backupBalance = _balance;

            try
            {
                _balance += value;

                AddTransaction(TransactionType.Deposit, value);
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

                AddTransaction(TransactionType.Transfer, value);
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

                AddTransaction(TransactionType.Payment, value);
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

                AddTransaction(TransactionType.Withdraw, value);

                return _balance;
            }
            catch
            {
                _balance = backupBalance;

                return 0;
            }
        }


        public void DefineCurrency(string isoCode) => _currency = Enumeration.GetItemByValue<Currency>(isoCode);
    }
}