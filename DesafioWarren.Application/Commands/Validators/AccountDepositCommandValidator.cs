using DesafioWarren.Infrastructure.Dapper.Queries;
using FluentValidation;

namespace DesafioWarren.Application.Commands.Validators
{
    public class AccountDepositCommandValidator : AbstractValidator<AccountDepositCommand>
    {
        public AccountDepositCommandValidator(IAccountQueries accountQueries)
        {
            RuleFor(command => command.Value)
                .GreaterThan(0);

            RuleFor(command => command.AccountId)
                .MustAsync(async (accountId, cancellationToken) =>
                {
                    var account = await accountQueries.GetAccountById(accountId, cancellationToken);

                    return account is not null;
                })
                .WithMessage("There's no account with the provided id.");
        }
    }

    public class AccountTransferCommandValidator : AbstractValidator<AccountTransferCommand>
    {
        public AccountTransferCommandValidator(IAccountQueries accountQueries)
        {
            
        }
    }

    public class AccountPaymentCommandValidator : AbstractValidator<AccountPaymentCommand>
    {

    }

    public class AccountWithdrawCommandValidator : AbstractValidator<AccountWithdrawCommand>
    {

    }
}