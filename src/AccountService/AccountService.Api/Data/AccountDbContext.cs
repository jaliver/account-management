using AccountService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Api.Data
{
    public class AccountDbContext : DbContext
    {
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }
    }
}
