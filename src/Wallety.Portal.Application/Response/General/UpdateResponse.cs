namespace Wallety.Portal.Application.Response.General
{
    public class UpdateResponse
    {
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public UpdateResponse() { }

        public static UpdateResponse Successs(string message)
        {
            return new UpdateResponse() { IsSuccess = true, ResponseMessage = message };
        }

        public static UpdateResponse Error(string message)
        {
            return new UpdateResponse() { IsSuccess = false, ResponseMessage = message };
        }
    }
}
