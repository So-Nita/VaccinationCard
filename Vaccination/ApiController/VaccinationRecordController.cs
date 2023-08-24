using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaccination_WebApi.EnityModel;
using Vaccination_WebApi.Model.VaccinationRecord;
using Vaccination_WebApi.UnitofWork;

namespace Vaccination_WebApi.Controllers
{
    [ApiController]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public VaccinationRecordController(IUnitOfWork context)
        {
            _context= context;  
        }

        // GET: VaccinationRecordController
        [HttpGet("get")]
        public IActionResult Get()
        {
            var data =  _context.GetRepository<VaccinatedRecord, string>().GetAll()!.Select(e => new VaccinatedRecord
            {
                Id= e.Id,
                Customer_Id=e.Customer_Id,  
                Card_Id=e.Card_Id,  
                DoeseReceived=e.DoeseReceived,  
                DateVaccinated=e.DateVaccinated,
                Customer = e.Customer
            });
            return Ok(data);
        }

        // POST: VaccinationRecordController/Create
        [HttpPost("createVaccinateRecord")]
        public IActionResult Create(VaccinationRecordModel model)
        {
            try 
            {
                var customer = _context.GetRepository<Customer, string>().GetAll()!.Where(e => e.IdentityId == model.Cus_IdentityId).FirstOrDefault();
                if(customer == null) 
                {
                    return NotFound("Customer does not existing.Plase create new customer profile");
                }
                var vaccRecord = _context.GetRepository<VaccinatedRecord, string>().GetAll()!.Where(e => e.Customer_Id.Contains(customer!.Id)).FirstOrDefault();
                var returnData = new List<VaccinatedRecord>();  
                _context.BeginTransaction();
                // New record
                if (vaccRecord == null)
                {
                    var cusRecord = new VaccinatedRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Customer_Id = customer.Id,
                        Card_Id = model.Card_Id,
                        DoeseReceived = model.DoeseReceived,
                        DateVaccinated = model.DateVaccinated
                    };
                    returnData.Add(cusRecord);
                    _context.GetRepository<VaccinatedRecord, string>().AddAsync(cusRecord);
                    _context.Commit();
                }
                else
                {
                    vaccRecord.Customer_Id = customer.Id;
                    vaccRecord.Card_Id = model.Card_Id;
                    vaccRecord.DoeseReceived= model.DoeseReceived;  
                    vaccRecord.DateVaccinated= model.DateVaccinated;
                    
                    returnData.Add(vaccRecord);
                    _context.GetRepository<VaccinatedRecord, string>().Update(vaccRecord);
                    _context.Commit();

                }
                return Ok(returnData);
            }
            catch
            {
                return BadRequest(new Exception());
            }

        }

        // GET: VaccinationRecordController/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }*/


        // GET: VaccinationRecordController/Delete/5
        /* public ActionResult Delete(int id)
         {
             return View();
         }*/

    }
}
