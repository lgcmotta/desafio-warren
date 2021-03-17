using System;
using DesafioWarren.Application.Models;
using MediatR;

namespace DesafioWarren.Application.Commands
{
    public abstract class AccountIdCommand : IRequest<Response>
    {
        public Guid AccountId { get; }

        protected AccountIdCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}