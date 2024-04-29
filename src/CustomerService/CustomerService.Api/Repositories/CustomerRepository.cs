using CustomerService.Api.Data;
using CustomerService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _dbContext.Set<Customer>().AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomer(string fullName)
        {
            return await _dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Name != null && x.Name.Equals(fullName));
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var updatedCustomer = _dbContext.Set<Customer>().Update(customer);
            await _dbContext.SaveChangesAsync();
            return updatedCustomer.Entity;
        }
    }
}
