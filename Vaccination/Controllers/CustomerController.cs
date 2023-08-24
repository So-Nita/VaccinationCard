using Microsoft.AspNetCore.Mvc;

namespace Vaccination.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
