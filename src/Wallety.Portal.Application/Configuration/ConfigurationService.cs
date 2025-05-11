using Microsoft.Extensions.Configuration;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Application.Configuration
{
    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string ConnectionString()
        {
            return Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                ?? GetConnectionString("PGSQL_CONNECTION_STRING");
        }

        public string AllowedCreditWalletAccounts()
        {
            return Environment.GetEnvironmentVariable("ALLOWED_CREDIT_WALLET_ACCOUNTS")
                ?? GetValue("ALLOWED_CREDIT_WALLET_ACCOUNTS");
        }

        public string SecretKey()
        {
            return Environment.GetEnvironmentVariable("SECRET_KEY")
                ?? GetValue("SECRET_KEY");
        }

        public int CreditLimit()
        {
            var creditLimit = Environment.GetEnvironmentVariable("CREDIT_LIMIT")
                ?? GetValue("CREDIT_LIMIT");

            return int.TryParse(creditLimit, out var result) ? result : 50000;
        }

        public string CustomerPortalApi()
        {
            return Environment.GetEnvironmentVariable("CUSTOMER_PORTAL_API")
                ?? GetValue("CUSTOMER_PORTAL_API");
        }


        private string GetValue(string configName)
        {
            return _configuration.GetValue<string>($"Values:{configName}")!;
        }

        private string GetConnectionString(string configName)
        {
            return _configuration.GetValue<string>($"ConnectionStrings:{configName}")!;
        }
    }
}
