using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Api.Controllers
{
    public class LookupController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [Authorize]
        [HttpGet]
        [Route("Country")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Country()
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
                new LookupParams()
                {
                    LookupTableName = LookupTableSchema.CountryTable,
                    LookupPrimaryKey = LookupTableSchema.CountryPrimaryKey,
                    LookupName = LookupTableSchema.CountryName,
                    UseCustomQuery = true,
                }
            ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));

        }

        [Authorize]
        [HttpGet]
        [Route("RegistrationStatuses")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegistrationStatuses([FromQuery] bool isFilter)
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
                new LookupParams()
                {
                    LookupTableName = LookupTableSchema.RegistrationStatusTable,
                    LookupPrimaryKey = LookupTableSchema.RegistrationStatusPrimaryKey,
                    LookupName = LookupTableSchema.RegistrationStatusName,
                    IsFilter = isFilter
                }
            ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }

        [Authorize]
        [HttpGet]
        [Route("VerificationRejectReasons")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> VerificationRejectReasons()
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
                new LookupParams()
                {
                    LookupTableName = LookupTableSchema.VerificationRejectReasonsTable,
                    LookupPrimaryKey = LookupTableSchema.VerificationRejectReasonsPrimaryKey,
                    LookupName = LookupTableSchema.VerificationRejectReasonsName
                }
            ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }

        [Authorize]
        [HttpGet]
        [Route("IsActive")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public IActionResult IsActive()
        {
            var result = new List<LookupResponse>() {
                    new() { Id = Guid.NewGuid(), Name = "Active", AltBoolValue = true },
                    new() { Id = Guid.NewGuid(), Name = "Inactive", AltBoolValue = false  }
                };

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(new DataList<LookupResponse>()
            {
                Items = result,
                Count = result.Count
            }));
        }

        [Authorize]
        [HttpGet]
        [Route("Roles")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Roles([FromQuery] bool isFilter)
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
               new LookupParams()
               {
                   LookupTableName = LookupTableSchema.AspNetRolesTable,
                   LookupPrimaryKey = LookupTableSchema.AspNetRolesPrimaryKey,
                   LookupName = LookupTableSchema.AspNetRolesName,
                   IsFilter = isFilter
               }
           ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }

        [Authorize]
        [HttpGet]
        [Route("TransactionTypes")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TransactionTypes()
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
                new LookupParams()
                {
                    LookupTableName = LookupTableSchema.TransactionTypesTable,
                    LookupPrimaryKey = LookupTableSchema.TransactionTypesPrimaryKey,
                    LookupName = LookupTableSchema.TransactionTypesName
                }
            ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }

        [Authorize]
        [HttpGet]
        [Route("PaymentOptions")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PaymentOptions()
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
               new LookupParams()
               {
                   LookupTableName = LookupTableSchema.PaymentOptionsTable,
                   LookupPrimaryKey = LookupTableSchema.PaymentOptionsPrimaryKey,
                   LookupName = LookupTableSchema.PaymentOptionsName
               }
           ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }

        [Authorize]
        [HttpGet]
        [Route("Products")]
        [ProducesResponseType(typeof(BaseResponse), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> Products()
        {
            var items = await _mediator.Send(new LookupQuery<LookupResponse>(
                new LookupParams()
                {
                    LookupTableName = LookupTableSchema.ProductsTable,
                    LookupPrimaryKey = LookupTableSchema.ProductsPrimaryKey,
                    LookupName = LookupTableSchema.ProductsName
                }
            ));

            return Ok(ReturnSuccessModel<DataList<LookupResponse>>(items));
        }
    }
}
