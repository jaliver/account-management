
using AccountManagement.Api.CustomerService;
using AccountManagement.Api.Settings;

namespace AccountManagement.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISettings _settings;

        public CustomerService(IHttpClientFactory httpClientFactory, ISettings settings)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory, nameof(httpClientFactory));
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));

            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        public async Task CreateCustomer(string customerFullName)
        {
            var customerServiceClient = GetCustomerServiceClient();

            await customerServiceClient.CustomerPOSTAsync(customerFullName);
        }

        public async Task<Customer> GetCustomer(string customerFullName)
        {
            var customerServiceClient = GetCustomerServiceClient();

            return await customerServiceClient.CustomerGETAsync(customerFullName);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var customerServiceClient = GetCustomerServiceClient();

            await customerServiceClient.CustomerPUTAsync(customer);
        }

        private CustomerServiceClient GetCustomerServiceClient()
        {
            var customerServiceBaseUrl = _settings.GetStringSetting("ConnectionStrings:CustomerServiceBaseUrl");

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(customerServiceBaseUrl);

            return new CustomerServiceClient(httpClient);
        }
    }
}
