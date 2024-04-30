using AccountManagement.Api.CustomerService;

namespace AccountManagement.Api.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(string customerFullName);
        Task<Customer> GetCustomer(string customerFullName);
        Task UpdateCustomer(Customer customer);
    }
}
