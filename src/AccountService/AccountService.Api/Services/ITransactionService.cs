using AccountService.Api.Models;

namespace AccountService.Api.Services
{
    public interface ITransactionService
    {
        Task CreateDepositTransaction(SavingsAccount savingsAccount, decimal amount);
        Task CreateWithdrawalTransaction(SavingsAccount savingsAccount, decimal amount);
        IEnumerable<Transaction> GetTransactions(int savingsAccountId);
    }
}
