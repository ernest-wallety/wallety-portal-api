namespace Wallety.Portal.Core.Services
{
    public interface IConfigurationService
    {
        string AllowedCreditWalletAccounts();
        string ConnectionString();
        string SecretKey();
        int CreditLimit();
        string CustomerPortalApi();
    }
}
