using System;
using System.Collections.Generic;
using DesafioWarren.Domain.Entities;

namespace DesafioWarren.Domain.Aggregates
{
    public class Account : Entity, IAggregateRoot
    {
        private string _name;

        private string _email;
        
        private string _phoneNumber;
        
        private AccountBalance _accountBalance;
        
        public string Cpf { get; }
        
        public string GetName() => _name;

        public string GetEmail() => _email;

        public string GetPhoneNumber() => _phoneNumber;

        public void ChangeEmail(string email) => _email = email;

        public void ChangePhoneNumber(string phoneNumber) => _phoneNumber = phoneNumber;
        
        private Account() { }

        public Account(string name, string email, string phoneNumber, string cpf)
        {
            Id = Guid.NewGuid();
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            Cpf = cpf;
            _accountBalance = new AccountBalance();
        }

        public void Deposit(decimal value) => _accountBalance.Deposit(value);

        public void Transfer(Account destination, decimal value) => _accountBalance.Transfer(destination, value);

        public void Payment(decimal value) => _accountBalance.Payment(value);

        public decimal Withdraw(decimal value) => _accountBalance.Withdraw(value);

        public decimal GetBalance() => _accountBalance.GetBalance();

        public IEnumerable<AccountTransaction> GetAccountTransactions() => _accountBalance.Transactions;

    }
}