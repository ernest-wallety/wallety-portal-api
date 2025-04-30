namespace Wallety.Portal.Core.Results
{
    public class DeleteRecordResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public DeleteRecordResult() { }

        public static DeleteRecordResult Successs()
        {
            return new DeleteRecordResult() { IsSuccess = true };
        }

        public static DeleteRecordResult Error(Exception ex)
        {
            return new DeleteRecordResult() { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
}
