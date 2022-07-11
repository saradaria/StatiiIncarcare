using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
using System.Linq;

namespace StatiiIncarcare.Controllers
{
    public class DisponibilitateController : Controller
    {
        private readonly IncarcareStatiiContext _context;

        public DisponibilitateController(IncarcareStatiiContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int idPriza)
        {
            WeekCalendar saptamana = new WeekCalendar();
            saptamana.Week = new List<RezCalendar>();


            DateTime startingDay = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var rezervariPriza = _context.Rezervares
                .Where(p => p.IdPriza == idPriza && p.TimeIn.Date >= startingDay  && p.TimeIn.Date <=startingDay.AddDays(7))
                .ToList();

            foreach (var x in rezervariPriza)
            {
                RezCalendar rezervare = new RezCalendar();
                rezervare.DataIn = x.TimeIn.ToString("HH:mm");
                rezervare.DataOut = x.TimeOut.ToString("HH:mm");
                rezervare.NrMasina = x.NrMasina;
                rezervare.ZiCurenta = x.TimeIn.DayOfWeek;
                saptamana.Week.Add(rezervare);

            }

            return View(saptamana);
        }

    }
}
        

    public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}

