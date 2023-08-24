using Microsoft.AspNetCore.Mvc;
using Vaccination_WebApi.EnityModel;
using Vaccination_WebApi.Model.Customer;
using Vaccination_WebApi.UnitofWork;

namespace Vaccination_WebApi.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public CustomerController(IUnitOfWork context)
        {
            _context= context;  
        }
        [HttpGet("getAllCustomer")]
        public IActionResult Index()
        {
            var customer = _context.GetRepository<Customer,string>().GetAll()!.Select(e=> new Customer
                            {
                                Id= e.Id,
                                Name=e.Name,    
                                DOB=e.DOB,
                                IdentityId = e.IdentityId,
                                Province=e.Province
                            }).ToList(); 
            return Ok(customer);
        }
        [HttpPost("addCustomer")]
        public async Task<IActionResult> Post(CustomerCreateModel customer)
        {
            var newCus = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = customer.Name,
                DOB = customer.DOB,
                ProvinceId = customer.ProvinceId
            };
            _context.BeginTransaction();
            try
            {
                await _context.GetRepository<Customer, string>().AddAsync(newCus);
                _context.Commit();  
                return Ok(newCus);
            }
            catch
            {
                _context.Rollback();    
                return StatusCode(401,new Exception());
            }
        }

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> Put(CustomerUpdateModel data)
        {
            try
            {
                var customer = await _context.GetRepository<Customer, string>().GetByIdAsync(data.Id);
                if (customer == null) return NotFound("Customer does not exist");
               
                customer.Name = data.Name;
                customer.DOB = data.DOB;
                customer.IdentityId = data.IndentityId;
                customer.ProvinceId = data.ProvinceId;

                _context.BeginTransaction();
                await _context.GetRepository<Customer, string>().UpdateAsync(customer);
                _context.Commit();
                return Ok(customer);
            }
            catch
            {
                return BadRequest(new Exception());
            }
        }

    }
}
