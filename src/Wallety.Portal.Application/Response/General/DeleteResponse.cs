namespace Wallety.Portal.Application.Response.General
{
    public class DeleteResponse
    {
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public DeleteResponse() { }

        public static DeleteResponse Successs(string message)
        {
            return new DeleteResponse() { IsSuccess = true, ResponseMessage = message };
        }

        public static DeleteResponse Error(string message)
        {
            return new DeleteResponse() { IsSuccess = false, ResponseMessage = message };
        }
    }
}
