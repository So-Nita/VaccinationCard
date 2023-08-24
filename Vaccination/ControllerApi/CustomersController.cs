using Vaccination.EnityModel;
using Vaccination.Models.Customer;
using Vaccination.UnitofWork;
using System.Web.Http;

namespace Vaccination.ControllerApi
{
    [Route("/api/Customers")]
    public class CustomersController : ApiController
    {
        private readonly IUnitOfWork _context;
        public CustomersController(IUnitOfWork context)
        {
            _context= context;  
        }
        //[Route("getAllCustomer")]
        public IEnumerable<Customer> Get()
        {
            var customer = _context.GetRepository<Customer,string>().GetAll()!.Select(e=> new Customer
                            {
                                Id= e.Id,
                                Name=e.Name,    
                                DOB=e.DOB,
                                IdentityId = e.IdentityId,
                                Province=e.Province
                            }).ToList(); 
            return customer;
        }
        //[Route("addCustomer")]
        public object Post(CustomerCreateModel customer)
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
                 _context.GetRepository<Customer, string>().AddAsync(newCus);
                _context.Commit();  
                return Ok(newCus);
            }
            catch
            {
                _context.Rollback();
                throw new Exception();
            }
        }

        //[Route("updateCustomer")]
        public async Task<object> Put(CustomerUpdateModel data)
        {
            try
            {
                var customer = await  _context.GetRepository<Customer, string>().GetByIdAsync(data.Id);
                if (customer == null) return new Exception("Customer does not exist");
               
                customer.Name = data.Name;
                customer.DOB = data.DOB;
                customer.IdentityId = data.IndentityId;
                customer.ProvinceId = data.ProvinceId;

                _context.BeginTransaction();
                  _context.GetRepository<Customer, string>().UpdateAsync(customer);
                _context.Commit();
                return Ok(customer);
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
