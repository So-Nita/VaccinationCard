namespace Vaccination_WebApi.EnityModel
{
    public class VaccinatedRecord
    {
        public string Id { get; set; }  
        public string Customer_Id { get; set; } 
        public string Card_Id { get; set; } 
        public int DoeseReceived { get; set; }   
        public DateTime DateVaccinated { get; set; }
        public Customer Customer { get; set; }

    }
}
