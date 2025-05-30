using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Api.Controllers
{
    public class WatiController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [HttpGet]
        [Route("TemplateList")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TemplateList()
        {
            var result = await _mediator.Send(new ListAllQuery<WatiTemplateResponse>(null));

            return Ok(ReturnSuccessModel<DataList<WatiTemplateResponse>>(result));
        }

        [HttpGet("GetTemplate")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTemplate([FromQuery] string code)
        {
            var result = await _mediator.Send(new ItemGeneralQuery<WatiTemplateResponse>(code));

            return Ok(ReturnSuccessModel<WatiTemplateResponse>(result));
        }
    }
}
