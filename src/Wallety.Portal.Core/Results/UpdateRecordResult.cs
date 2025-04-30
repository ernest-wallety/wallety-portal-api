namespace Wallety.Portal.Core.Results
{
    public class UpdateRecordResult
    {
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public UpdateRecordResult() { }

        public static UpdateRecordResult Successs(string message)
        {
            return new UpdateRecordResult() { IsSuccess = true, ResponseMessage = message };
        }

        public static UpdateRecordResult Error(Exception ex)
        {
            return new UpdateRecordResult() { IsSuccess = false, ResponseMessage = ex.Message };
        }
    }
}
