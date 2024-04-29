using AccountService.Api.Models;

namespace AccountService.Api.Services
{
    public interface ISavingsAccountService
    {
        Task<bool> CreateSavingsAccount(int customerId);
        Task<SavingsAccount?> GetSavingsAccount(int customerId);
        Task<bool> DepositToSavingsAccount(int customerId, decimal amount);
        Task<bool> WithdrawFromSavingsAccount(int customerId, decimal amount);
    }
}
