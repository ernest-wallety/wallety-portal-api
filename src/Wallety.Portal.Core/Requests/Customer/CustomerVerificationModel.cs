namespace Wallety.Portal.Core.Requests.Customer
{
    public class CustomerVerificationModel
    {
        public string? CustomerId { get; set; }
        public string? RegistrationStatusId { get; set; }
        public string? VerificationRejectReasonId { get; set; }
    }
}
