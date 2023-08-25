using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using Vaccination.EnityModel;
using Vaccination.Model.VaccinationRecord;
using Vaccination.UnitofWork;

namespace Vaccination.ControllerApi
{
    [Route("/api/VaccinationRecords")]
    public class VaccinationRecordsController : ApiController
    {
        private readonly IUnitOfWork _context;
        public VaccinationRecordsController(IUnitOfWork context)
        {
            _context = context;  
        }

        public IEnumerable<object> Get()
        {
            var vaccRecords = _context.GetRepository<VaccinatedRecord, string>().GetAll().ToList();
            var cardTypes = _context.GetRepository<CardType, string>().GetAll().ToList();
            var provinces = _context.GetRepository<Province, string>().GetAll().ToList();
            var customers = _context.GetRepository<Customer, string>().GetAll().ToList();

           
            var result = (from vr in vaccRecords
                          join c in customers on vr.Customer_Id equals c.Id
                          join ct in cardTypes on vr.Card_Id equals ct.Id
                          join p in provinces on c.ProvinceId equals p.Id
                          group new { c.Id, ct.CardName } by new { p.ProvinceName, vr.DoeseReceived } into g
                          select new
                          {
                              ProvinceName = g.Key.ProvinceName,
                              DoseReceived = g.Key.DoeseReceived,
                              CustomersCount = g.Select(item => item.Id).Distinct().Count(),
                              CardTypes = new 
                              {
                                  MOD = g.Count(item => item.CardName == "MOD"),
                                  MOH = g.Count(item => item.CardName == "MOH")
                              }
                          }).ToList();
            var t = result;
            return result;
        }

        public IEnumerable<object> Create(VaccinationRecordModel model)
        {
            try 
            {
                var customer = _context.GetRepository<Customer, string>().GetAll()!.Where(e => e.IdentityId == model.Cus_IdentityId).FirstOrDefault();
                if(customer == null) 
                {
                    throw new Exception("Customer does not existing.Plase create new customer profile");
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
                return returnData;
            }
            catch
            {
                throw new Exception();
            }

        }
        

    }
}
