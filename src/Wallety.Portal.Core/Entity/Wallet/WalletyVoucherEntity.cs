namespace Wallety.Portal.Core.Entity.Wallet
{
    public class WalletyVoucherEntity
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Guid WalletTransactionHistoryId { get; set; }
        public bool IsRedeemed { get; set; }
        public Int32 VoucherPin { get; set; }
        public decimal PreviousWalletBalance { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal VoucherAmount { get; set; }
    }
}
