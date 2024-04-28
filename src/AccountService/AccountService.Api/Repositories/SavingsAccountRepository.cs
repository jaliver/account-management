using AccountService.Api.Data;

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
    }
}
