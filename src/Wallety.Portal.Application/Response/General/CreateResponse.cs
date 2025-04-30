namespace Wallety.Portal.Application.Response.General
{
    public class CreateResponse
    {
        public Guid? ReturnRecordId { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public CreateResponse() { }

        public static CreateResponse Successs(Guid id)
        {
            return new CreateResponse() { IsSuccess = true, ReturnRecordId = id };
        }

        public static CreateResponse Error(Exception ex)
        {
            return new CreateResponse() { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
}
