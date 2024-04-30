using AccountManagement.Api.AccountService;

namespace AccountManagement.Api.Services
{
    public interface IAccountService
    {
        Task CreateSavingsAccount(int customerId);
        Task DepositToSavingsAccount(int customerId, double amount);
        Task WithdrawFromSavingsAccount(int customerId, double amount);
        Task<SavingsAccount> GetSavingsAccount(int customerId);
    }
}
