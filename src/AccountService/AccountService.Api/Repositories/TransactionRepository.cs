using AccountService.Api.Data;

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
    }
}
