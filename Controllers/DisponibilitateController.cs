using Microsoft.AspNetCore.Mvc;

namespace StatiiIncarcare.Controllers
{
    public class DisponibilitateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
