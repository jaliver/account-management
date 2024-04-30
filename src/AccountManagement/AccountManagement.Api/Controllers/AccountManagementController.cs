using AccountManagement.Api.AccountService;
using AccountManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountManagementController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountManagementController(ILogger<AccountManagementController> logger, IAccountManagementService accountManagementService)
        {
            ArgumentNullException.ThrowIfNull(accountManagementService, nameof(accountManagementService));
            
            _accountManagementService = accountManagementService;
        }

        [HttpPost("savings-account/{customerFullName}")]
        public async Task<IActionResult> CreateSavingsAccount(string customerFullName)
        {
            await _accountManagementService.CreateSavingsAccount(customerFullName);
            return Ok();
        }

        [HttpPut("deposit/{customerFullName}/{amount}")]
        public async Task<IActionResult> Deposit(string customerFullName, double amount)
        {
            await _accountManagementService.DepositToSavingsAccount(customerFullName, amount);
            return Ok();
        }

        [HttpPut("withdrawal/{customerFullName}/{amount}")]
        public async Task<IActionResult> Withdraw(string customerFullName, double amount)
        {
            await _accountManagementService.WithdrawFromSavingsAccount(customerFullName, amount);
            return Ok();
        }

        [HttpGet("savings-account-balance/{customerFullName}")]
        public async Task<ActionResult<double>> GetSavingsAccountBalance(string customerFullName)
        {
            var balance = await _accountManagementService.GetSavingsAccountBalance(customerFullName);
            return Ok(balance);
        }

        [HttpGet("transactions/{customerFullName}/{numberOfTransactions}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(string customerFullName, int numberOfTransactions)
        {
            var transactions = await _accountManagementService.GetTransactions(customerFullName, numberOfTransactions);
            return Ok(transactions);
        }
    }
}
