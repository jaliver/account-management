using CustomerService.Api.Models;

namespace CustomerService.Api.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(string fullName, int savingsAccountId);
        Task<Customer?> GetCustomer(string fullName);
    }
}
