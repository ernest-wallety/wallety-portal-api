using System.Text.Json.Serialization;

namespace Wallety.Portal.Core.Entity
{
    public class TransactionHistoryEntity
    {
        public Guid? TransactionHistoryId { get; set; }

        public string TransactionReference { get; set; }

        public Guid? UserSessionId { get; set; }
        public string? UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public float Amount { get; set; }

        public Guid? TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }

        public Guid? ProductId { get; set; }
        public string ProductIconPath { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }

        public Guid? BeneficiaryId { get; set; }
        public string BeneficiaryWhatsappNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string OnceOffBeneficiaryName { get; set; }
        public string OnceOffBeneficiaryWhatsappNumber { get; set; }
        public string OnceOffBeneficiaryDisplayNumber { get; set; }

        public string? TransactionDescription { get; set; }
        public string? TransactionBeneficiaryPayer { get; set; }

        public Guid? PaymentOptionId { get; set; }
        public string PaymentOptionName { get; set; }
        public string PaymentOptionIconPath { get; set; }

        public string PayerWhatsappNumber { get; set; }

        public DateTime TransactionDate { get; set; }
        public string TransactionMessage { get; set; }

        public Guid? TransactionProcessStatusId { get; set; }
        public string TransactionStatus { get; set; }

        public bool IsComplete { get; set; }

        [JsonIgnore]
        public string TransactionHistoryJson { get; set; }
    }
}
