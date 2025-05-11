using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.General;

namespace Wallety.Portal.Api.Controllers
{
    public class WalletController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;


        [Authorize(Roles = "Executive")]
        [HttpPost]
        [Route("CreditWallet")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UserRoleUpdate([FromBody] CreditWalletDTO request)
        {
            var result = await _mediator.Send(new UpdateCommand<CreditWalletDTO, UpdateResponse>(request));

            return Ok(ReturnSuccessModel<UpdateResponse>(result, result.ResponseMessage!, (int)HttpStatusCode.OK, true, 0));
        }



    }
}
