using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Enum;

namespace Wallety.Portal.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public object ReturnSuccessModel<T>(
           T? data,
           string message = "Success",
           int statusCode = StatusCodes.Status200OK,
           bool showSuccess = false,
           EnumValidationDisplay errorDisplay = EnumValidationDisplay.Popup,
           bool showError = true)
        {
            return new BaseResponse
            {
                Data = data,
                ResponseMessage = message,
                StatusCode = statusCode,
                ErrorDisplay = errorDisplay,
                ShowSuccess = showSuccess,
                ShowError = showError,
                IsError = false
            };
        }
    }
}
