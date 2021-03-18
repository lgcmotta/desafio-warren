using AutoMapper;
using DesafioWarren.Application.Models;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Entities;
using DesafioWarren.Domain.ValueObjects;
using FluentValidation.Results;

namespace DesafioWarren.Application.AutoMapper.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Account, AccountModel>()
                .ForMember(accountModel => accountModel.Balance
                    , options => options.MapFrom(account => account.GetBalance()))
                .ForMember(accountModel => accountModel.CurrencySymbol
                    , options => options.MapFrom(account => account.GetCurrencySymbol()))
                .ForMember(accountModel => accountModel.Currency
                    , options => options.MapFrom(account => account.GetCurrencyIsoCode()))
                .ReverseMap()
                .ConstructUsing(accountModel => new Account(accountModel.Name
                    , accountModel.Email
                    , accountModel.PhoneNumber
                    , accountModel.Cpf
                    , Enumeration.GetItemByValue<Currency>(accountModel.Currency)));

            CreateMap<Account, AccountModelBase>()
                .ForMember(accountModel => accountModel.Currency
                    , options => options.MapFrom(account => account.GetCurrencyIsoCode()))
                .ReverseMap()
                .ConstructUsing(accountModel => new Account(accountModel.Name
                    , accountModel.Email
                    , accountModel.PhoneNumber
                    , accountModel.Cpf
                    , Enumeration.GetItemByValue<Currency>(accountModel.Currency))); 

            CreateMap<AccountTransaction, AccountTransactionModel>()
                .ReverseMap();

            CreateMap<ValidationFailure, Failure>().ReverseMap();
        }
    }
}