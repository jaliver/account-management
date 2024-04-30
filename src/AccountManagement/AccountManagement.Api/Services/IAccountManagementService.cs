using AccountManagement.Api.AccountService;

namespace AccountManagement.Api.Services
{
    public interface IAccountManagementService
    {
        Task CreateSavingsAccount(string customerFullName);
        Task DepositToSavingsAccount(string customerFullName, double amount);
        Task WithdrawFromSavingsAccount(string customerFullName, double amount);
        Task<double> GetSavingsAccountBalance(string customerFullName);
        Task<IEnumerable<Transaction>> GetTransactions(string customerFullName, int numberOfTransactions);
    }
}
