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
        /*
        public IActionResult Index(int idPriza)
        {
            WeekCalendar saptamana = new WeekCalendar();

            DateTime startingDay = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            int i = 0;
            var rezervariPriza = _context.Rezervares.Where(p => p.IdPriza == idPriza && p.TimeIn >= startingDay  && p.TimeIn <=startingDay.AddDays(7)).ToList();

            foreach (var x in rezervariPriza)
            {
                DayCalendar zi = new DayCalendar();
                zi.DataIn = x.TimeIn.ToString("MM/dd/yyyy H:mm");
                zi.DataOut = x.TimeOut.ToString("MM/dd/yyyy H:mm");
                zi.NrMasina = x.NrMasina;
                saptamana.Add(zi);
            }

            return View();
        }

*/

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

