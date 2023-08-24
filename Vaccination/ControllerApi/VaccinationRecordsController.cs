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
            _context= context;  
        }

        public IEnumerable<object> Get()
        {
            var records = _context.GetRepository<VaccinatedRecord, string>().GetAll().Include(e => e.Customer).ToList();
            var allCard = _context.GetRepository<CardType, string>().GetAll().ToList();
            var provinces = _context.GetRepository<Province, string>().GetAll().ToList();

            var data = new List<object>(); 

            var groupByDose = records.GroupBy(e => e.DoeseReceived).ToList();
            foreach (var cusRecord in groupByDose)
            {
                foreach (var record in cusRecord)
                {
                    var provinName = provinces.FirstOrDefault(e => e.Id == record.Customer.ProvinceId)?.ProvinceName;
                    var cardName = allCard.FirstOrDefault(e => e.Id == record.Card_Id)?.CardName;

                    var _temp = new 
                    {
                        ProvinceName = provinName,
                        NumOfDose = record.DoeseReceived,
                        SumVisitor = "", 
                        CardTypes = new
                        {
                            CardName = cardName,
                            NumVacc = cusRecord.Count() 
                        }
                    };

                    data.Add(_temp); 
                }
            }

            return data; 
        }


        // POST: VaccinationRecordController/Create
        //[HttpPost("createVaccinateRecord")]
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

     
        public class GetVacciRecordModel
        {
            public string ProvinceName { get; set; }
            public int NumOfDose { get; set; }
            public string SumVisitor { get; set; }
            public IEnumerable<string> CardTypes { get; set; } 
        }
    }
}
