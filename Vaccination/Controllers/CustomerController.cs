using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vaccination.EnityModel;
using Vaccination.Models.Customer;
using Vaccination.UnitofWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vaccination.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _context;
        public CustomerController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult CustomerIndexPage()
        {
            var customer = _context.GetRepository<Customer, string>().GetAll()!.Select(e => new CustomerGetModel
            {
                Id = e.Id,
                Name = e.Name,
                DOB = e.DOB,
                IdentityId = e.IdentityId,
                Province = e.Province
            }).ToList();
            return View(customer);
        }
        
        [HttpGet]
        public IActionResult AddCustomer()
        {
            CustomerCreateViewModel model = new CustomerCreateViewModel();
            model.CardTypes = new List<SelectListItem>();
            model.ProvinceList = new List<SelectListItem>();

            var cardTypes =  _context.GetRepository<CardType, string>().GetAll()!.ToList();
            var provinces = _context.GetRepository<Province, string>().GetAll()!.ToList();

            foreach (var card in cardTypes)
            {
                var _temp = new SelectListItem
                {
                    Text = card.CardName,
                    Value = card.Id
                };
                model.CardTypes.Add(_temp);
            }
            foreach (var card in provinces)
            {
                var _temp1 = new SelectListItem
                {
                    Text = card.ProvinceName,
                    Value = card.Id
                };
                model.ProvinceList.Add(_temp1);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateModel data) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Note : All field is required.";
                return View();
            }
            var newCus = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = data.Name,
                DOB = data.DOB,
                ProvinceId = data.ProvinceId,
                IdentityId = data.IndentityId
            };
            var newRecord = new VaccinatedRecord
            {
                Id = Guid.NewGuid().ToString(),
                Customer_Id = newCus.Id,
                Card_Id = data.CardId,
                DoeseReceived = data.DoseReceived,
                DateVaccinated = data.DateVaccinated,
            };
           
            _context.BeginTransaction();
            try
            {
                await _context.GetRepository<Customer, string>().AddAsync(newCus);
                await _context.GetRepository<VaccinatedRecord, string>().AddAsync(newRecord);
                _context.Commit();
                TempData["SuccessMessage"] = "Successfully.";
                return Ok("Create successfully");
            }
            catch
            {
                _context.Rollback();
               return View("AddCustomer");
            }
        }

        [HttpGet]
        public IActionResult ViewCustomer(Guid id)
        {
            var models = new CustomerCreateViewModel();
            var cardTypes = _context.GetRepository<CardType, string>().GetAll()!.ToList();
            var provinces = _context.GetRepository<Province, string>().GetAll()!.ToList();
            var customer = _context.GetRepository<Customer, string>().GetAll()!.Where(e => e.Id == id.ToString()).Include(e=>e.VaccinatedRecords)
                            .Select(e => new CustomerUpdateModel
                            {
                                CustomerId= e.Id,
                                Name = e.Name,
                                ProvinceId = e.Province.Id,
                                IndentityId = e.IdentityId,
                                DOB = e.DOB,
                                CardId = e.VaccinatedRecords.FirstOrDefault()!.Card_Id,
                                DoseReceived = e.VaccinatedRecords.FirstOrDefault()!.DoeseReceived,
                                DateVaccinated = e.VaccinatedRecords.FirstOrDefault()!.DateVaccinated,  
                            }).ToList();
            models.Customer = customer.FirstOrDefault()!;
            models.ProvinceList = provinces.Select(e=>new SelectListItem { Text= e.ProvinceName,Value=e.Id}).ToList();
            models.CardTypes = cardTypes.Select(e => new SelectListItem { Text = e.CardName, Value = e.Id }).ToList();

            return View(models);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateModel data)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error";
                return View();
            }

            var customer = await _context.GetRepository<Customer, string>().GetByIdAsync(data.CustomerId);

            customer.Name = data.Name;
            customer.DOB = data.DOB;
            customer.IdentityId = data.IndentityId;
            customer.ProvinceId = data.ProvinceId;
            
            var vaccinRecord = _context.GetRepository<VaccinatedRecord, string>().GetAll()!.Where(e=>e.Customer_Id==customer.Id).First();

            vaccinRecord.DateVaccinated = data.DateVaccinated;
            vaccinRecord.DoeseReceived = data.DoseReceived;
            vaccinRecord.Card_Id= data.CardId;  
            
            _context.BeginTransaction();
            try
            {
                await _context.GetRepository<VaccinatedRecord, string>().UpdateAsync(vaccinRecord);
                await _context.GetRepository<Customer, string>().UpdateAsync(customer);
                _context.Commit();
                // return await Task.Run(()=> View("CustomerIndexPage", customer));
                TempData["SuccessMessage"] = "Successfully.";
                return Ok(data);   
            }
            catch
            {
                return View("View");
            }
        }
    }
}
