using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerServiceController : ControllerBase
    {

        private readonly ILogger<CustomerServiceController> _logger;

        public CustomerServiceController(ILogger<CustomerServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost("customer/{fullName}")]
        public IActionResult CreateCustomer(string fullName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("customer/{fullName}")]
        public IActionResult GetCustomer(string fullName)
        {
            throw new NotImplementedException();
        }
    }
}
