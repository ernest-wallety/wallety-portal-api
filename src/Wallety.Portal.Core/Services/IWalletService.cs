using Wallety.Portal.Core.Entity.User;

namespace Wallety.Portal.Core.Services
{
    public interface IWalletService
    {
        Task CreditWallet(UserEntity entity, decimal topUpAmount);
    }
}
