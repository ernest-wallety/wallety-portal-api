using Wallety.Portal.Core.Entity.User;

namespace Wallety.Portal.Core.Entity.Customer
{
    public class CustomerEntity : UserEntity
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
