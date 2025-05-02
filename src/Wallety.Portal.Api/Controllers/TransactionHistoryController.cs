using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Api.Controllers
{
    public class TransactionHistoryController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [Authorize]
        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers([FromQuery] BaseListCriteria criteria)
        {
            var result = await _mediator.Send(new ListQuery<TransactionHistoryResponse>(criteria));

            return Ok(ReturnSuccessModel<Pagination<TransactionHistoryResponse>>(result));
        }

        [Authorize]
        [HttpGet("Get")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByReference(string reference)
        {
            var result = await _mediator.Send(new ListAllQuery<TransactionHistoryResponse>(reference));

            return Ok(ReturnSuccessModel<DataList<TransactionHistoryResponse>>(result));

        }
    }
}
