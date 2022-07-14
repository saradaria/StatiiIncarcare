using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using StatiiIncarcare.Utils;

namespace StatiiIncarcare.Controllers
{
    public class ChartController : Controller
    {
        private readonly IncarcareStatiiContext _context;


        public ChartController(IncarcareStatiiContext context)
        {
            _context = context;
        }

        public ActionResult Index(int id)
        {
            List<Point> dataPoints = new List<Point>();
            List<DateTime> dateTimes = new List<DateTime>();
            DateTime day = DateTime.Now.StartOfWeek(DayOfWeek.Monday);

            for (int i = 0; i < 7; i++)
            {
                dateTimes.Add(day.AddDays(i));
            }

            foreach (var item in dateTimes)
            {
                var rezeravari = _context.Rezervares.Where(r => r.TimeIn.Date == item.Date);
                var nrRezervari = _context.Rezervares
                    .Include(p => p.IdPrizaNavigation)
                    .Where(p => p.IdPrizaNavigation.IdStatie == id && p.TimeIn.Date == item.Date).Count();
                Point newPoint = new Point(item.DayOfWeek.ToString() +"(" +item.Date.ToShortDateString()+")", nrRezervari);
                dataPoints.Add(newPoint);
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();

        }
    }
}