using System;
using System.Threading.Tasks;
using DesafioWarren.Api.Filters;
using DesafioWarren.Application.Commands;
using DesafioWarren.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWarren.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiExceptionFilter]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAccountAsync([FromBody] CreateAccountCommand createAccountCommand)
        {
            var response = await _mediator.Send(createAccountCommand);

            return ReturnCreated(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAccountAsync([FromBody] UpdateAccountCommand updateAccountCommand)
        {
            var response = await _mediator.Send(updateAccountCommand);

            return ReturnOk(response);
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> PostDepositAsync([FromRoute] Guid accountId, [FromBody] decimal value)
        {
            var response = await _mediator.Send(new AccountDepositCommand(accountId, value));

            return ReturnAccepted(response);
        }

        [HttpPut("{accountId}/transfer")]
        public async Task<IActionResult> PutTransferAsync([FromRoute] Guid accountId, [FromBody] decimal value)
        {
            var response = await _mediator.Send(new AccountTransferCommand(accountId, value));

            return ReturnAccepted(response);
        }

        [HttpPut("{accountId}/payment")]
        public async Task<IActionResult> PutPaymentAsync([FromRoute] Guid accountId, [FromBody] decimal value)
        {

            var response = await _mediator.Send(new AccountPaymentCommand(accountId, value));

            return ReturnAccepted(response);
        }

        [HttpPut("{accountId}/withdraw")]
        public async Task<IActionResult> PutWithdrawAsync([FromRoute] Guid accountId, [FromBody] decimal value)
        {
            var response = await _mediator.Send(new AccountWithdrawCommand(accountId, value));

            return ReturnAccepted(response);
        }

        private IActionResult ReturnOk(Response response)
        {
            return FormatActionResult(Ok(response), response);
        }

        private IActionResult ReturnCreated(Response response)
        {
            return FormatActionResult(Created(response.GetResponsePath()
                    , response)
                , response);
        }

        private IActionResult ReturnAccepted(Response response)
        {
            return FormatActionResult(Accepted(response), response);
        }
        
        private IActionResult FormatActionResult(IActionResult actionResult, Response response)
        {
            return response.IsErrorResponse() ? BadRequest(response) : actionResult;
        }

    }
}