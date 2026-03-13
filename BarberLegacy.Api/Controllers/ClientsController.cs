using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
