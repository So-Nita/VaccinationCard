using Microsoft.AspNetCore.Mvc.Rendering;
using Vaccination.EnityModel;

namespace Vaccination.Models.Customer
{
    public class CustomerCreateViewModel
    {
        public CustomerUpdateModel Customer { get; set; }
        public List<SelectListItem> CardTypes { get; set; }
        public List<SelectListItem> ProvinceList { get; set; }
    }
    public class CustomerUpdateModel : CustomerCreateModel
    {
        public string CustomerId { get; set; }  
    }


}
