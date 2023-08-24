using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Vaccination_WebApi.UnitofWork;

namespace Vaccination_WebApi.EnityModel
{
    public class Customer
    {
        public string Id { get; set; }  
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int IdentityId { get; set; }
        public string ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<VaccinatedRecord> VaccinatedRecords { get; set; }
    }
}
