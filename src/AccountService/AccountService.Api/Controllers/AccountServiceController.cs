using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountServiceController : ControllerBase
    {
        private readonly ILogger<AccountServiceController> _logger;

        public AccountServiceController(ILogger<AccountServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost("savings-account/{customerId}")]
        public IActionResult CreateSavingsAccount(int customerId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("savings-account/{customerId}")]
        public IActionResult GetSavingsAccount(int customerId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("deposit-to-savings-account/{savingsAccountId}/{amount}")]
        public IActionResult DepositToSavingsAccount(int savingsAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        [HttpPut("withdraw-from-savings-account/{savingsAccountId}/{amount}")]
        public IActionResult WithdrawFromSavingsAccount(int savingsAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
