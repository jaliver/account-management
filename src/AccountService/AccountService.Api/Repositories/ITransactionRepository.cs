using AccountService.Api.Models;

namespace AccountService.Api.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransaction(Transaction transaction);
        IEnumerable<Transaction> GetTransactions(int savingsAccountId);
    }
}
