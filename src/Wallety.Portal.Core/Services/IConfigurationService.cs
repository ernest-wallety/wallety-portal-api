namespace Wallety.Portal.Core.Services
{
    public interface IConfigurationService
    {
        string AllowedCreditWalletAccounts();
        string ConnectionString();
        string SecretKey();
        string CreditLimit();
    }
}
