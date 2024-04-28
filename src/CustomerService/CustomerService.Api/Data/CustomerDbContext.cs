using CustomerService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
    }
}
