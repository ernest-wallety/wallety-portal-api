namespace Wallety.Portal.Core.Entity.Customer
{
    public class VerificationRejectReasonsEntity
    {
        public Guid RejectReasonId { get; set; }
        public string Reason { get; set; }
        public string RejectMessage { get; set; }
    }
}
