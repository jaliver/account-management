using AccountService.Api.Data;
using AccountService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Api.Repositories
{
    public class SavingsAccountRepository : ISavingsAccountRepository
    {
        private readonly AccountDbContext _dbContext;

        public SavingsAccountRepository(AccountDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public async Task AddSavingsAccount(SavingsAccount savingsAccount)
        {
            await _dbContext.Set<SavingsAccount>().AddAsync(savingsAccount);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SavingsAccount?> GetSavingsAccount(int customerId)
        {
            return await _dbContext.Set<SavingsAccount>().FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId));
        }

        public async Task<SavingsAccount> UpdateSavingsAccount(SavingsAccount savingsAccount)
        {
            var updatedSavingsAccount = _dbContext.Set<SavingsAccount>().Update(savingsAccount);
            await _dbContext.SaveChangesAsync();
            return updatedSavingsAccount.Entity;
        }
    }
}
