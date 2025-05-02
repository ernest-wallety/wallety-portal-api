namespace Wallety.Portal.Core.Results
{
    public class DeleteRecordResult
    {
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public DeleteRecordResult() { }

        public static DeleteRecordResult Successs(string message)
        {
            return new DeleteRecordResult() { IsSuccess = true, ResponseMessage = message };
        }

        public static DeleteRecordResult Error(string message)
        {
            return new DeleteRecordResult() { IsSuccess = false, ResponseMessage = message };
        }
    }
}
