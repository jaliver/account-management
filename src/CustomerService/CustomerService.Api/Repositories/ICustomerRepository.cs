using CustomerService.Api.Models;

namespace CustomerService.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
        Task<Customer?> GetCustomer(string fullName);
    }
}
