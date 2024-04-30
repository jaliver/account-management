using AccountManagement.Api.AccountService;

namespace AccountManagement.Api.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public AccountManagementService(ICustomerService customerService, IAccountService accountService)
        {
            ArgumentNullException.ThrowIfNull(customerService, nameof(customerService));
            ArgumentNullException.ThrowIfNull(accountService, nameof(accountService));

            _customerService = customerService;
            _accountService = accountService;
        }

        public async Task CreateSavingsAccount(string customerFullName)
        {
            await _customerService.CreateCustomer(customerFullName);
            
            var customer = await _customerService.GetCustomer(customerFullName);

            await _accountService.CreateSavingsAccount(customer.Id);

            var savingsAccount = _accountService.GetSavingsAccount(customer.Id);

            customer.SavingsAccountId = savingsAccount.Id;

            await _customerService.UpdateCustomer(customer);
        }

        public async Task DepositToSavingsAccount(string customerFullName, double amount)
        {
            var customer = await _customerService.GetCustomer(customerFullName);
            await _accountService.DepositToSavingsAccount(customer.Id, amount);
        }

        public async Task WithdrawFromSavingsAccount(string customerFullName, double amount)
        {
            var customer = await _customerService.GetCustomer(customerFullName);
            await _accountService.WithdrawFromSavingsAccount(customer.Id, amount);
        }

        public async Task<double> GetSavingsAccountBalance(string customerFullName)
        {
            var customer = await _customerService.GetCustomer(customerFullName);

            var savingsAccount = await _accountService.GetSavingsAccount(customer.Id);

            return savingsAccount.Balance;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string customerFullName, int numberOfTransactions)
        {
            var customer = await _customerService.GetCustomer(customerFullName);

            var savingsAccount = await _accountService.GetSavingsAccount(customer.Id);

            return savingsAccount.Transactions.OrderByDescending(x => x.CreatedDate).Take(numberOfTransactions);
        }
    }
}
