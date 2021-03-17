using AutoMapper;
using DesafioWarren.Application.Models;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Entities;

namespace DesafioWarren.Application.AutoMapper.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Account, AccountModel>()
                .ForMember(accountModel => accountModel.Balance
                    , options => options.MapFrom(account => account.GetBalance()))
                .ReverseMap();

            CreateMap<AccountTransaction, AccountTransactionModel>()
                .ReverseMap();
        }
    }
}