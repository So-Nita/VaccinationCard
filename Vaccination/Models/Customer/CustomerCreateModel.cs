namespace Vaccination_WebApi.Model.Customer
{
    public class CustomerCreateModel
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string ProvinceId { get; set; }
        public int IndentityId { get; set; }
    }
}
