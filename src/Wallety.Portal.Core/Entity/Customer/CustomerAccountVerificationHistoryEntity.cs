namespace Wallety.Portal.Core.Entity.Customer
{
    public class CustomerAccountVerificationHistoryEntity
    {
        public string VerificationHistoryId { get; set; }
        public string Status { get; set; }
        public string RejectionReason { get; set; }
        public string UpdatedBy { get; set; }
        public string Date { get; set; }
    }
}
