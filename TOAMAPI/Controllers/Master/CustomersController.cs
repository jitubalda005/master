using Domain.Entities.Customers;
using Domain.Interfaces;
using Domain.Interfaces.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TOEMAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            var customerObj = await customerRepository.GetAllAsync();
            return Ok(customerObj);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerByID(int id)
        {
            var customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();

            var customerObj = await customerRepository.GetByIdAsync(id);
            if (customerObj == null)
            {
                return NotFound();
            }
            return Ok(customerObj);
        }
        [HttpPost]

        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            var customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();

            await customerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetCustomerByID), new { id = customer.ID }, customer);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> EditCustomer(int id, Customer customer)
        {

            if (id != customer.ID)
            {
                return BadRequest();
            }

            var customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            customerRepository.Update(customer);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            var customerObj = await customerRepository.GetByIdAsync(id);
            if (customerRepository == null)
            {
                return NotFound();
            }

            customerRepository.Remove(customerObj);
            await _unitOfWork.CompleteAsync();
            return NoContent();


        }
    }
}

