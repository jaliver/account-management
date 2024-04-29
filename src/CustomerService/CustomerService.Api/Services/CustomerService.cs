using CustomerService.Api.Models;
using CustomerService.Api.Repositories;

namespace CustomerService.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            ArgumentNullException.ThrowIfNull(customerRepository, nameof(customerRepository));

            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomer(string fullName)
        {
            var existingCustomer = await GetCustomer(fullName);

            if (existingCustomer is not null)
            {
                return false;
            }

            var newCustomer = new Customer
            {
                Name = fullName
            };

            await _customerRepository.AddCustomer(newCustomer);
            
            return true;
        }

        public async Task<Customer?> GetCustomer(string fullName)
        {
            return await _customerRepository.GetCustomer(fullName);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            await _customerRepository.UpdateCustomer(customer);
            return true;
        }
    }
}
