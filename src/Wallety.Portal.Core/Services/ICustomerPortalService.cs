using Wallety.Portal.Core.Requests;

namespace Wallety.Portal.Core.Services
{
    public interface ICustomerPortalService
    {
        Task<dynamic> LoginPortal();

        Task<dynamic> CreditWallet(CreditWalletModel request);
    }
}
