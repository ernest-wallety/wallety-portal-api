namespace Wallety.Portal.Core.Entity
{
    public class WalletTransactionEntity
    {
        public Guid TransactionId { get; set; }

        public Guid? TransactionHistoryId { get; set; }

        //USER
        public string WalletyUserId { get; set; }
        public decimal Amount { get; set; }

        //TRANSACTION
        public Guid? TransactionTypeId { get; set; }

        public Guid TransactionStatusId { get; set; }

        public string? UserTransactionReferenceNumber { get; set; }
        public string? ReceiptNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? TransactionMessage { get; set; }

        //PAYMENT
        public Guid? PaymentOptionId { get; set; }
        public decimal ServiceFeeValue { get; set; }
        public decimal ServiceFeeAmount { get; set; }
        public decimal AmountAfterFeeCharge { get; set; }
        public string? RedeemedWalletyVoucherPin { get; set; }
        public Guid? FeeTypeId { get; set; }

        //PRODUCT
        public Guid? ProductId { get; set; }
        public string? ProductDisplayName { get; set; }
        public decimal? ProductDailyLimit { get; set; }
        public decimal? ProductMonthlyLimit { get; set; }
        public string? ProductReference { get; set; }

        //BENEFICIARY
        public Guid? BeneficiaryId { get; set; }
        public string? OnceOffBeneficiaryName { get; set; }
        public string? OnceOffBeneficiaryWhatsappNumber { get; set; }
        public string? OnceOffBeneficiaryDisplayNumber { get; set; }
        public Guid? UserSessionId { get; set; }

        public string? WalletyMePayerWhatsappNumber { get; set; }
        public Guid? WalletyMeRequestId { get; set; }

        //PAYMENT REQUEST
        public Guid? PaymentRequestId { get; set; }
    }
}
