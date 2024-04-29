using AccountService.Api.Models;
using AccountService.Api.Repositories;

namespace AccountService.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            ArgumentNullException.ThrowIfNull(transactionRepository, nameof(transactionRepository));
            
            _transactionRepository = transactionRepository;
        }

        public async Task CreateDepositTransaction(SavingsAccount savingsAccount, decimal amount)
        {
            await CreateTransaction(savingsAccount, amount, TransactionType.Deposit);
        }

        public async Task CreateWithdrawalTransaction(SavingsAccount savingsAccount, decimal amount)
        {
            await CreateTransaction(savingsAccount, amount, TransactionType.Withdrawal);
        }

        public IEnumerable<Transaction> GetTransactions(int savingsAccountId)
        {
            return _transactionRepository.GetTransactions(savingsAccountId);
        }

        private async Task CreateTransaction(SavingsAccount savingsAccount, decimal amount, TransactionType type)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                CreatedDate = DateTime.UtcNow,
                SavingsAccountId = savingsAccount.Id,
                SavingsAccount = savingsAccount,
                Type = type
            };

            await _transactionRepository.AddTransaction(transaction);
        }
    }
}
