namespace Vaccination_WebApi.Model.Customer
{
    public class CustomerUpdateModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int IndentityId { get; set; }
        public string ProvinceId { get; set; }
    }
}
