using CustomerService.Api.Models;
using CustomerService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerServiceController : ControllerBase
    {
        private readonly ILogger<CustomerServiceController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerServiceController(ILogger<CustomerServiceController> logger, ICustomerService customerService)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(customerService, nameof(customerService));

            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("customer/{fullName}")]
        public async Task<IActionResult> CreateCustomer(string fullName)
        {
            var customerCreated = await _customerService.CreateCustomer(fullName);

            if (!customerCreated)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpGet("customer/{fullName}")]
        public async Task<ActionResult<Customer>> GetCustomer(string fullName)
        {
            var customer = await _customerService.GetCustomer(fullName);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("customer")]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            await _customerService.UpdateCustomer(customer);

            return Ok();
        }
    }
}
