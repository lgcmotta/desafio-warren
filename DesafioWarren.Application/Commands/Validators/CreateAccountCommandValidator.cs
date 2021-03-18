using System;
using System.Linq;
using System.Linq.Expressions;
using DesafioWarren.Application.Extensions;
using DesafioWarren.Application.Models;
using DesafioWarren.Domain.ValueObjects;
using DesafioWarren.Infrastructure.Dapper.Queries;
using FluentValidation;

namespace DesafioWarren.Application.Commands.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator(IAccountQueries accountQueries)
        {
            RuleFor(command => command.Account.Name)
                .StringPropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Cpf)
                .StringPropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.PhoneNumber)
                .StringPropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Email)
                .StringPropertyMustNotBeNullOrEmpty();

            RuleFor(command => command.Account.Currency)
                .Must(currency => Enumeration.GetEnumerationItems<Currency>()
                    .Select(possibleCurrency => possibleCurrency.Value)
                    .Contains(currency));

            RuleFor(command => command.Account.Name)
                .MustAsync(async (name, cancellationToken) =>
                {
                    var account = await accountQueries.GetAccountByName(name, cancellationToken);

                    return account is null;
                })
                .WithMessage("There's already an account that belong to this person.");

            RuleFor(command => command.Account.Cpf)
                .MustAsync(async (cpf, cancellationToken) =>
                {
                    var account = await accountQueries.GetAccountByCpf(cpf, cancellationToken);

                    return account is null;
                }).WithMessage("This CPF already has an account.");
        }
        
        
    }

    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<TObject, string> StringPropertyMustNotBeNullOrEmpty<TObject>(this IRuleBuilderInitial<TObject, string> builderOptions)
        {
            return builderOptions.NotEmpty()
                .NotNull();
        }
    }
}