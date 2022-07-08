using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;

namespace StatiiIncarcare.Controllers
{
    public class BookingController : Controller
    {
        private readonly IncarcareStatiiContext _incarcareStatiiContext;

        public IActionResult Index()
        {
            return View();
        }

        public BookingController(IncarcareStatiiContext context)
        {
            _incarcareStatiiContext = context;
        }

        public IActionResult NewBooking(int idPriza)
        {
            var newBooking = new AddBooking();

            newBooking.IdPriza = idPriza;
            
            return View(newBooking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewBooking(AddBooking model)
        {
            if (ModelState.IsValid)
            {
                var booking = new Rezervare();
                booking.IdPriza = model.IdPriza;
                booking.TimeIn = model.TimeIn;
                if(booking.TimeIn < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(AddBooking.TimeIn), "Data selectata a trecut");
                    return View(model);
                }
                booking.TimeOut = model.TimeOut;
             
                if (booking.TimeIn > booking.TimeOut)
                {
                    ModelState.AddModelError(nameof(AddBooking.TimeOut), "Data finalizarii incarcarii este inaintea datei inceperii");
                    return View(model);
                }

                var eroareRezervarecase1 = _incarcareStatiiContext.Rezervares.Where(p => p.TimeIn <= model.TimeIn && p.TimeOut >= model.TimeIn && p.IdPriza == model.IdPriza).FirstOrDefault();
                var eroareRezervarecase2 = _incarcareStatiiContext.Rezervares.Where (p=> p.TimeIn>= model.TimeIn && p.TimeOut>=model.TimeIn && p.IdPriza == model.IdPriza).FirstOrDefault();
                var eroareRezervarecase3 = _incarcareStatiiContext.Rezervares.Where(p => p.TimeIn <= model.TimeIn && p.TimeOut >= model.TimeIn && p.IdPriza == model.IdPriza).FirstOrDefault();
                var eroareRezervarecase4 = _incarcareStatiiContext.Rezervares.Where(p => p.TimeIn >= model.TimeIn && p.TimeOut <= model.TimeIn && p.IdPriza == model.IdPriza).FirstOrDefault();


                if (eroareRezervarecase1 != null || eroareRezervarecase2 != null || eroareRezervarecase3 != null || eroareRezervarecase4 != null)
                {
                    ModelState.AddModelError(nameof(AddBooking.TimeOut), "Intervalul orar ales este indisponibil");
                    return View(model);
                }

                booking.NrMasina = model.NrMasina;
                _incarcareStatiiContext.Add(booking);
                _incarcareStatiiContext.SaveChanges();
                var priza = _incarcareStatiiContext.Prizas.Where(p => p.IdPriza == model.IdPriza).FirstOrDefault();
                return RedirectToAction("Details", "Statii", new { id = priza.IdStatie });
            }
            return View(model);
        }
    }

    

}
