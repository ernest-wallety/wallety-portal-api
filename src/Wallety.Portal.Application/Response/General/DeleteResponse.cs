namespace Wallety.Portal.Application.Response.General
{
    public class DeleteResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public DeleteResponse() { }

        public static DeleteResponse Successs()
        {
            return new DeleteResponse() { IsSuccess = true };
        }

        public static DeleteResponse Error(Exception ex)
        {
            return new DeleteResponse() { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
}
