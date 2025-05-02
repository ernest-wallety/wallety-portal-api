using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Commands;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.Auth;

namespace Wallety.Portal.Api.Controllers
{
    public class AuthController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(CreateLoginCommand requestLogin)
        {
            _logger.LogInformation($"Login by {requestLogin.Email}");

            var result = await _mediator.Send(requestLogin);

            _logger.LogInformation($"Login result for {requestLogin.Email} {result.Success}");

            return Ok(ReturnSuccessModel<LoginResponse>(result, "Login successful", (int)HttpStatusCode.OK, true, 0));
        }

        // [Authorize]
        // [HttpPost]
        // [Route("Logout")]
        // [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult> Logout()
        // {

        //     var result = await _mediator.Send(requestLogin);


        //     return Ok(ReturnSuccessModel<LoginResponse>(result, "Logout successful", (int)HttpStatusCode.OK, true, 0));
        // }
    }
}
