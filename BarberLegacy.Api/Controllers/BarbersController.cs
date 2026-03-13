using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    public class BarbersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
