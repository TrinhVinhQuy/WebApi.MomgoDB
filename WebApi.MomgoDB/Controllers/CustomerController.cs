using Microsoft.AspNetCore.Mvc;
using WebApi.MomgoDB.Models;
using WebApi.MomgoDB.Services;

namespace WebApi.MomgoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get() =>
            await _customerService.GetAsync();

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            await _customerService.CreateAsync(customer);

            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Customer customerIn)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customerIn.Id = customer.Id;

            await _customerService.UpdateAsync(id, customerIn);

            return CreatedAtRoute("GetCustomer", new { id = customerIn.Id.ToString() }, customerIn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.RemoveAsync(id);

            return Ok("Delete customer has been successfully!");
        }
    }
}
