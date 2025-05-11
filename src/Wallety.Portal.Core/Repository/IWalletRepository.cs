using Wallety.Portal.Core.Entity.Wallet;
using Wallety.Portal.Core.Results;

namespace Wallety.Portal.Core.Repository
{
    public interface IWalletRepository
    {
        Task<List<WalletyVoucherEntity>> GetWalletyVouchers();

        Task<WalletyVoucherEntity> GetWalletyVoucher(Int32 voucherPin);

        Task<CreateRecordResult> CreateWalletVoucher(WalletyVoucherEntity entity);
    }
}
