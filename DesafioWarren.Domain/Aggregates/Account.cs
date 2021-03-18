using System;
using System.Collections.Generic;
using DesafioWarren.Domain.Entities;
using DesafioWarren.Domain.ValueObjects;

namespace DesafioWarren.Domain.Aggregates
{
    public class Account : Entity, IAggregateRoot
    {
        private string _name;

        private string _email;
        
        private string _phoneNumber;
        
        private AccountBalance _accountBalance;
        
        public string Cpf { get; private set; }
        
        public string Name => _name;

        public string Email => _email;

        public string PhoneNumber => _phoneNumber;

        public void ChangeEmail(string email) => _email = email;

        public void ChangePhoneNumber(string phoneNumber) => _phoneNumber = phoneNumber;
        
        private Account() { }

        public Account(string name, string email, string phoneNumber, string cpf, Currency currency)
        {
            Id = Guid.NewGuid();
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            Cpf = cpf;
            _accountBalance = new AccountBalance(currency);
        }

        public void Deposit(decimal value) => _accountBalance.Deposit(value);

        public void Transfer(Account destination, decimal value) => _accountBalance.Transfer(destination, value);

        public void Payment(decimal value) => _accountBalance.Payment(value);

        public decimal Withdraw(decimal value) => _accountBalance.Withdraw(value);

        public string GetBalance() => $"{_accountBalance.Currency.Symbol}{decimal.Round(_accountBalance.Balance, 2, MidpointRounding.AwayFromZero)}";

        public IEnumerable<AccountTransaction> GetAccountTransactions() => _accountBalance.Transactions;

        public void CorrectCpf(string cpf) => Cpf = cpf;

        public void CorrectName(string name) => _name = name;

        public string GetCurrencySymbol() => _accountBalance.Currency.Symbol;

        public string GetCurrencyIsoCode() => _accountBalance.Currency.Value;

        public void DefineCurrency(string isoCode) => _accountBalance.DefineCurrency(isoCode);
    }
}