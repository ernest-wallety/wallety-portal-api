using Wallety.Portal.Core.Enum;

namespace Wallety.Portal.Application.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            ShowError = true;
            ErrorDisplay = EnumValidationDisplay.Popup;
            ShowSuccess = false;
            ResponseMessage = "Success";
        }

        public object? Data { get; set; }
        public bool ShowError { get; set; }
        public int StatusCode { get; set; }
        public bool ShowSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public EnumValidationDisplay ErrorDisplay { get; set; }
    }
}
