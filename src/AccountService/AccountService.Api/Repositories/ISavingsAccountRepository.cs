using AccountService.Api.Models;

namespace AccountService.Api.Repositories
{
    public interface ISavingsAccountRepository
    {
        Task AddSavingsAccount(SavingsAccount savingsAccount);
        Task<SavingsAccount?> GetSavingsAccount(int customerId);
        Task<SavingsAccount> UpdateSavingsAccount(SavingsAccount savingsAccount);
    }
}
