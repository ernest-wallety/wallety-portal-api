using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Wallety.Portal.Api.Exceptions.CustomException;
using Wallety.Portal.Core.Enum;

namespace Wallety.Portal.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ErrorTitleProvider.GetErrorTitle(ex),
                Detail = ex.Message,
                Type = ex.GetType().Name,
                Instance = httpContext.Request.Path.ToString(),
                Status = httpContext.Response.StatusCode,
                Extensions =
                {
                    ["traceID"] = Guid.NewGuid().ToString(),
                    ["raw"] = ex.ToString(),
                    ["isError"] = true,
                    ["errorDisplay"] = ex.Data["errorDisplay"] ?? EnumValidationDisplay.Popup,
                    ["showException"] = true,
                    ["errorList"] = new List<string>() { ex.Message },
                    ["isException"] = true
                }
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
