using CustomerService.Api.Models;

namespace CustomerService.Api.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(string fullName);
        Task<Customer?> GetCustomer(string fullName);
        Task<bool> UpdateCustomer(Customer customer);
    }
}
