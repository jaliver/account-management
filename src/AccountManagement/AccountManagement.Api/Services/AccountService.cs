
using AccountManagement.Api.AccountService;
using AccountManagement.Api.Settings;

namespace AccountManagement.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISettings _settings;

        public AccountService(IHttpClientFactory httpClientFactory, ISettings settings)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory, nameof(httpClientFactory));
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));

            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        public async Task CreateSavingsAccount(int customerId)
        {
            var accountServiceClient = GetAccountServiceClient();
            await accountServiceClient.SavingsAccountPOSTAsync(customerId);
        }

        public async Task DepositToSavingsAccount(int customerId, double amount)
        {
            var accountServiceClient = GetAccountServiceClient();
            await accountServiceClient.DepositToSavingsAccountAsync(customerId, amount);
        }

        public async Task WithdrawFromSavingsAccount(int customerId, double amount)
        {
            var accountServiceClient = GetAccountServiceClient();
            await accountServiceClient.WithdrawFromSavingsAccountAsync(customerId, amount);
        }

        public async Task<SavingsAccount> GetSavingsAccount(int customerId)
        {
            var accountServiceClient = GetAccountServiceClient();
            return await accountServiceClient.SavingsAccountGETAsync(customerId);
        }

        private AccountServiceClient GetAccountServiceClient()
        {
            var accountServiceBaseUrl = _settings.GetStringSetting("ConnectionStrings:AccountServiceBaseUrl");

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(accountServiceBaseUrl);

            return new AccountServiceClient(httpClient);
        }
    }
}
