namespace Vaccination_WebApi.Model.VaccinationRecord
{
    public class VaccinationRecordCreateModel
    {
        public string Customer_Id { get; set; }
        public string Card_Id { get; set; }
        public int DoeseReceived { get; set; }
        public DateTime DateVaccinated { get; set; }
    }
    public class VaccinationRecordModel
    {
        public int Cus_IdentityId { get; set; }
        public string Card_Id { get; set; }
        public int DoeseReceived { get; set; }
        public DateTime DateVaccinated { get; set; }
    }
}
