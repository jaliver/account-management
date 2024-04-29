using AccountService.Api.Models;
using AccountService.Api.Repositories;

namespace AccountService.Api.Services
{
    public class SavingsAccountService : ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly ITransactionService _transactionService;

        public SavingsAccountService(ISavingsAccountRepository savingsAccountRepository, ITransactionService transactionService)
        {
            ArgumentNullException.ThrowIfNull(savingsAccountRepository, nameof(savingsAccountRepository));
            ArgumentNullException.ThrowIfNull(transactionService, nameof(transactionService));

            _savingsAccountRepository = savingsAccountRepository;
            _transactionService = transactionService;
        }

        public async Task<bool> CreateSavingsAccount(int customerId)
        {
            var existingSavingsAccount = await _savingsAccountRepository.GetSavingsAccount(customerId);

            if (existingSavingsAccount is not null)
            {
                return false;
            }

            var newSavingsAccount = new SavingsAccount
            {
                Balance = 0,
                CustomerId = customerId
            };

            await _savingsAccountRepository.AddSavingsAccount(newSavingsAccount);

            return true;
        }

        public async Task<SavingsAccount?> GetSavingsAccount(int customerId)
        {
            return await _savingsAccountRepository.GetSavingsAccount(customerId);
        }

        // TODO either create result type or throw different exceptions to expose errors
        public async Task<bool> DepositToSavingsAccount(int customerId, decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            var savingsAccount = await GetSavingsAccount(customerId);

            if (savingsAccount is null)
            {
                return false;
            }

            await _transactionService.CreateDepositTransaction(savingsAccount, amount);

            return await UpdateSavingsAccountWithLatestTransaction(customerId);
        }

        // TODO either create result type or throw different exceptions to expose errors
        public async Task<bool> WithdrawFromSavingsAccount(int customerId, decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            var savingsAccount = await GetSavingsAccount(customerId);

            if (savingsAccount is null)
            {
                return false;
            }

            await _transactionService.CreateWithdrawalTransaction(savingsAccount, amount);

            return await UpdateSavingsAccountWithLatestTransaction(customerId);
        }

        // TODO either create result type or throw different exceptions to expose errors
        private async Task<bool> UpdateSavingsAccountWithLatestTransaction(int customerId)
        {
            var savingsAccount = await _savingsAccountRepository.GetSavingsAccount(customerId);

            if (savingsAccount is null)
            {
                return false;
            }

            var transactions = _transactionService.GetTransactions(savingsAccount.Id);
            var latestTransaction = transactions.MaxBy(x => x.CreatedDate);

            if (latestTransaction is null)
            {
                return false;
            }

            savingsAccount.Transactions.Add(latestTransaction);
            await _savingsAccountRepository.UpdateSavingsAccount(savingsAccount);

            return true;
        }
    }
}
