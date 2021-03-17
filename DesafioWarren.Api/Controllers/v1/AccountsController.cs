using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWarren.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiVersion("1.0")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAccountAsync()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAccountAsync()
        {
            return Ok();
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> PostDepositAsync([FromRoute] Guid accountId)
        {
            return Ok();
        }

        [HttpPut("{accountId}/transfer")]
        public async Task<IActionResult> PutTransferAsync([FromRoute] Guid accountId)
        {
            return Ok();
        }

        [HttpPut("{accountId}/payment")]
        public async Task<IActionResult> PutPaymentAsync([FromRoute] Guid accountId)
        {
            return Ok();
        }

        [HttpPut("{accountId}/withdraw")]
        public async Task<IActionResult> PutWithdrawAsync([FromRoute] Guid accountId)
        {
            return Ok();
        }

    }
}