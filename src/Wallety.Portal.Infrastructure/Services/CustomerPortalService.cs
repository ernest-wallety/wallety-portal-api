using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Wallety.Portal.Core.Requests;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Infrastructure.Services
{
    public class CustomerPortalService(
        IConfigurationService config,
        ICachingInMemoryService caching) : ICustomerPortalService
    {
        private readonly HttpClient _httpClient = new();

        // private readonly IConfigurationService _config = config;
        private readonly ICachingInMemoryService _caching = caching;

        private readonly string BASE_URL = config.CustomerPortalApi();

        public async Task<dynamic> LoginPortal()
        {
            _httpClient.BaseAddress = new Uri(BASE_URL);

            var payload = new { Email = _caching.Get<string?>("Email"), Password = _caching.Get<string?>("Password") };
            var jsonString = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Portal/Login", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            _caching.Set("CustomerPortalLoginResponse", responseContent, TimeSpan.FromDays(1));

            return JsonConvert.DeserializeObject<dynamic>(responseContent)!;
        }

        public async Task<dynamic> CreditWallet(CreditWalletModel request)
        {
            InitializeHttpClient();

            var payload = new
            {
                WhatsappNumber = request.WhatsappNumber,
                RoleCode = request.RoleCode,
                Amount = request.Amount
            };

            var jsonString = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Wallet/CreditWallet", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<dynamic>(responseContent)!;
        }

        private void InitializeHttpClient()
        {
            var loginResponse = _caching.Get<string?>("CustomerPortalLoginResponse");

            var token = JsonConvert.DeserializeObject<dynamic>(loginResponse!)?.Data?.SessionToken;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string?)token);
        }
    }
}
