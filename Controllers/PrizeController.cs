using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;

namespace StatiiIncarcare.Controllers
{
    public class PrizeController : Controller
    {
        private readonly IncarcareStatiiContext  _incarcareStatiiContext;

        public PrizeController(IncarcareStatiiContext context)
        {
            _incarcareStatiiContext = context;
        }


        public IActionResult AddPriza(int idStatie)
        {
            var addPriza = new AddPlugModel()
            {
                IdStatie = idStatie,
                TipPrize = _incarcareStatiiContext.Tips.ToList()
            };

            return View(addPriza);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddPriza(AddPlugModel model)
        {
            var priza= new Priza();
            priza.IdStatie = model.IdStatie;
            priza.IdTip = model.IdTip;
            _incarcareStatiiContext.Add(priza);
            _incarcareStatiiContext.SaveChanges();
            return RedirectToAction("Details", "Statii", new {id = model.IdStatie});
        }

    }
}
