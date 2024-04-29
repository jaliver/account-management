using AccountService.Api.Models;
using AccountService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountServiceController : ControllerBase
    {
        private readonly ILogger<AccountServiceController> _logger;
        private readonly ISavingsAccountService _savingsAccountService;

        public AccountServiceController(ILogger<AccountServiceController> logger, ISavingsAccountService savingsAccountService)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(savingsAccountService, nameof(savingsAccountService));

            _logger = logger;
            _savingsAccountService = savingsAccountService;
        }

        [HttpPost("savings-account/{customerId}")]
        public async Task<IActionResult> CreateSavingsAccount(int customerId)
        {
            var savingsAccountCreated = await _savingsAccountService.CreateSavingsAccount(customerId);

            if (!savingsAccountCreated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("savings-account/{customerId}")]
        public async Task<ActionResult<SavingsAccount>> GetSavingsAccount(int customerId)
        {
            var savingsAccount = await _savingsAccountService.GetSavingsAccount(customerId);

            if (savingsAccount is null)
            {
                return NotFound();
            }

            return Ok(savingsAccount);
        }

        [HttpPut("deposit-to-savings-account/{customerId}/{amount}")]
        public async Task<IActionResult> DepositToSavingsAccount(int customerId, decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest();
            }

            var depositSucceeded = await _savingsAccountService.DepositToSavingsAccount(customerId, amount);

            if (!depositSucceeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("withdraw-from-savings-account/{customerId}/{amount}")]
        public async Task<IActionResult> WithdrawFromSavingsAccount(int customerId, decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest();
            }

            var depositSucceeded = await _savingsAccountService.WithdrawFromSavingsAccount(customerId, amount);

            if (!depositSucceeded)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
