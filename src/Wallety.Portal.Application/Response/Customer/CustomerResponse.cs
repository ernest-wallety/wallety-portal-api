using Wallety.Portal.Application.Response.User;

namespace Wallety.Portal.Application.Response.Customer
{
    public class CustomerResponse : UserResponse
    {
        public Guid? WalletId { get; set; }
        public decimal UsableBalance { get; set; }

        public Guid? TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }

        public string LastActivityDescription { get; set; }
        public string LastActivityColour { get; set; }
        public string LastActivityStatus { get; set; }
    }
}
