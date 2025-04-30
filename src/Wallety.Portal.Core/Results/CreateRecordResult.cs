namespace Wallety.Portal.Core.Results
{
    public class CreateRecordResult
    {
        public Guid? ReturnRecordId { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public CreateRecordResult() { }

        public static CreateRecordResult Successs(Guid id)
        {
            return new CreateRecordResult() { IsSuccess = true, ReturnRecordId = id };
        }

        public static CreateRecordResult Error(Exception ex)
        {
            return new CreateRecordResult() { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
}
