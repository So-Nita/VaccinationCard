using Vaccination.EnityModel;

namespace Vaccination.Models.Customer
{
    public class CustomerGetModel
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public DateTime DOB { get; set; }   
        public int IdentityId { get; set; } 
        public Province Province { get; set; }
    }
}
