using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Entity.Wallet;

namespace Wallety.Portal.Core.Services
{
    public interface IWalletService
    {
        Task<WalletyVoucherEntity> CreditWallet(UserEntity entity, decimal topUpAmount);
    }
}
