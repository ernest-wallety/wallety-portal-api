using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Commands;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Queries;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.Auth;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Application.Response.Menu;
using Wallety.Portal.Core.Specs;

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

            return Ok(ReturnSuccessModel<LoginResponse>(result, result.ResponseMessage!, (int)HttpStatusCode.OK, true, 0));
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new CreateLogoutCommand());

            return Ok(ReturnSuccessModel<UpdateResponse>(result, "Logout successful!", (int)HttpStatusCode.OK, true, 0));
        }

        [Authorize]
        [HttpGet]
        [Route("MenuStructure")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MenuStructure()
        {
            var result = await _mediator.Send(new ListAllQuery<MenuResponse>(null));

            return Ok(ReturnSuccessModel<DataList<MenuResponse>>(result));
        }

        [Authorize]
        [HttpGet]
        [Route("RefreshUser")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshUser()
        {
            var result = await _mediator.Send(new GetLoginCredentialsQuery());

            return Ok(ReturnSuccessModel<LoginResponse>(result, "User refreshed successfully!", (int)HttpStatusCode.OK, true, 0));
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("OneTimePassword")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCompanyPacakge([FromBody] PasswordResetDTO dto)
        {
            var result = await _mediator.Send(new UpdateCommand<PasswordResetDTO, UpdateResponse>(dto));

            return Ok(ReturnSuccessModel<UpdateResponse>(result, $"Password reset email has been sent to {dto.Email}!", (int)HttpStatusCode.OK, true, 0));

        }
    }
}
