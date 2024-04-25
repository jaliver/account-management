using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SavingsAccountController : ControllerBase
    {
        private readonly ILogger<SavingsAccountController> _logger;

        public SavingsAccountController(ILogger<SavingsAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("savings-account")]
        public IActionResult CreateSavingsAccount(string customerFirstName, string customerSurname)
        {
            throw new NotImplementedException();
        }

        [HttpPut("deposit")]
        public IActionResult Deposit(int accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        [HttpPut("withdrawal")]
        public IActionResult Withdraw(int accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        [HttpGet("savings-account-balance")]
        public IActionResult GetSavingsAccountBalance(int accountNumber)
        {
            throw new NotImplementedException();
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactions(int accountNumber, int numberOfTransactions)
        {
            throw new NotImplementedException();
        }
    }
}
