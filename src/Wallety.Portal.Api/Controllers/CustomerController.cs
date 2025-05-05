using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.Customer;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.Customer;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Api.Controllers
{
    public class CustomerController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [Authorize(Roles = "Executive, Admin")]
        [HttpGet("List")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers([FromQuery] BaseListCriteria criteria)
        {
            var result = await _mediator.Send(new ListStatsQuery<CustomersOverviewResponse>(criteria));

            return Ok(ReturnSuccessModel<CustomersOverviewResponse>(result));
        }

        [Authorize]
        [HttpGet("GetUnverifiedCustomers")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUnverifiedAccounts([FromQuery] BaseListCriteria criteria)
        {
            var result = await _mediator.Send(new ListQuery<CustomerVerifyResponse>(criteria));

            return Ok(ReturnSuccessModel<Pagination<CustomerVerifyResponse>>(result));
        }

        [HttpPut("UnFreezeAccount")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnFreezeAccount([FromBody] string whatsappNumber)
        {
            // var response = await _customerRepository.UnFreezeAccountAsync(whatsappNumber);

            // var result = new UpdateResponseDTO() { IsSuccess = response.StatusCode == (HttpStatusCode)StatusCodes.Status201Created || response.StatusCode == (HttpStatusCode)StatusCodes.Status202Accepted };

            // if (response.StatusCode == HttpStatusCode.OK) await _watiService.SendConfirmationMessageAsync(whatsappNumber, "Your Wallety account has been reactivated/unfrozen. Thank you for being a valued customer of Wallety.");

            // return Ok(ReturnSuccessModel<UpdateResponseDTO>(result, response.Result, (int)response.StatusCode, true, 0));

            return Ok("");
        }

        [Authorize]
        [HttpPost("VerifyAccount")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> VerifyAccount(CustomerVerificationDTO request)
        {
            var result = await _mediator.Send(new CreateCommand<CustomerVerificationDTO, CreateResponse>(request));

            return Ok(ReturnSuccessModel<CreateResponse>(result, result.ResponseMessage!, (int)HttpStatusCode.OK, true, 0));
        }
    }
}
