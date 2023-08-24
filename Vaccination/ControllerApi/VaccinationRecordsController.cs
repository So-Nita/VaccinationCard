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
            var cardTypes = _context.GetRepository<CardType, string>().GetAll().ToList().ToList();
            var provinces = _context.GetRepository<Province, string>().GetAll().ToList().ToList();
            var customers = _context.GetRepository<Customer, string>().GetAll().ToList().ToList();

            var result = (from vr in vaccRecords
                          join c in customers on vr.Customer_Id equals c.Id
                          join ct in cardTypes on vr.Card_Id equals ct.Id
                          join p in provinces on c.ProvinceId equals p.Id
                          select new
                          {
                              RecordId = vr.Id,
                              CustomerId = vr.Customer_Id,
                              CardId = vr.Card_Id,
                              DoeseReceived = vr.DoeseReceived,
                              DateVaccinated = vr.DateVaccinated,
                              CardName = ct.CardName,
                              DOB = c.DOB,
                              ProvinceId = c.ProvinceId,
                              IdentityId = c.IdentityId,
                              CardCreate = ct.Create,
                              CardDelete = ct.Deleted,
                              ProvinceName = p.ProvinceName,
                              ProvinceDelete = p.Deleted,
                          }).ToList();
            // Group By Province
            var datagroupByProvince = result.GroupBy(c => new { c.ProvinceName, c.DoeseReceived }).ToList();
            //Group By Dose
            var ilist = new List<object>();

            var subCountCard = new List<object>();
            foreach (var i in datagroupByProvince)
            {
                var tt = i.GroupBy(e => e.DoeseReceived).ToList();
                ilist.Add(tt);
                var item = i.Count();
                var cardName = i.First().CardName;
                var group = i.GroupBy(e => e.CardName).ToList();
                var card = new CardTypes
                {
                    CardName = cardName,
                    SumCartType = item
                };
                subCountCard.Add(card);
            }

            return datagroupByProvince;
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

        class RecordModel
        {
            public string ProvinceName { get; set; }
            public int ReceiveDose { get; set; }
            public int SumVisitor { get; set; }
            public List<CardTypes> CardTypes_ { get; set; }
        }
        class CardTypes
        {
            public string CardName { get; set; }
            public int SumCartType { get; set; }

        }
    }
}
