using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Api.Controllers
{
    public class UserController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers([FromQuery] BaseListCriteria criteria)
        {
            var result = await _mediator.Send(new ListQuery<UserResponse>(criteria));

            return Ok(ReturnSuccessModel<Pagination<UserResponse>>(result));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCompanyById(Guid id)
        {
            var result = await _mediator.Send(new ItemQuery<UserResponse>(id));

            return Ok(ReturnSuccessModel<UserResponse>(result));
        }

        [Authorize]
        [HttpPut]
        [Route("UserRoleUpdate")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCompanyPacakge([FromBody] UserRoleUpdateDTO request)
        {
            var result = await _mediator.Send(new UpdateCommand<UserRoleUpdateDTO, UpdateResponse>(request));

            return Ok(ReturnSuccessModel<UpdateResponse>(result, $"Password reset email has been sent to {request.Email}!", (int)HttpStatusCode.OK, true, 0));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] UserEntity request)
        {
            var result = await _mediator.Send(new CreateCommand<UserEntity, UpdateResponse>(request));

            return Ok(ReturnSuccessModel<UpdateResponse>(null, result.ResponseMessage!, (int)HttpStatusCode.OK, true, 0));
        }
    }
}
