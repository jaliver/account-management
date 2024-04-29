using AccountService.Api.Data;
using AccountService.Api.Models;

namespace AccountService.Api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AccountDbContext _dbContext;

        public TransactionRepository(AccountDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _dbContext.Set<Transaction>().AddAsync(transaction);
            var result = await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Transaction> GetTransactions(int savingsAccountId)
        {
            return _dbContext.Set<Transaction>().Where(x => x.SavingsAccountId.Equals(savingsAccountId));
        }
    }
}
