﻿using System.Linq;
using DesafioWarren.Domain.ValueObjects;
using DesafioWarren.Infrastructure.Dapper.Queries;
using FluentValidation;

namespace DesafioWarren.Application.Commands.Validators
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator(IAccountQueries accountQueries)
        {
            RuleFor(command => command.AccountId)
                .PropertyMustNotBeNullOrEmpty()
                .MustAsync(async (accountId, cancellationToken) =>
                {
                    var account = await accountQueries.GetAccountById(accountId, cancellationToken);

                    return account is not null;
                })
                .WithMessage("There's no account with the provided id.");

            RuleFor(command => command.Account.Name)
                .PropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Cpf)
                .PropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.PhoneNumber)
                .PropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Email)
                .PropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Currency)
                .Must(currency => Enumeration.GetEnumerationItems<Currency>()
                    .Select(possibleCurrency => possibleCurrency.Value)
                    .Contains(currency));
        }
    }
}