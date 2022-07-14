using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
using StatiiIncarcare.Utils;
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

        // actiune : 1 - next week ; -1 prev week
        public IActionResult Index(int idPriza, int actiune)
        {
            WeekCalendar saptamana = new WeekCalendar();
            saptamana.Week = new List<RezCalendar>();

            if (actiune == 0)
            {
                DateTimeUtils.StartingDay = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            }


            if (actiune == 1)
            {
                DateTimeUtils.StartingDay = DateTimeUtils.StartingDay.AddDays(7);
            }

            if (actiune == -1)
            {
                DateTimeUtils.StartingDay = DateTimeUtils.StartingDay.AddDays(-7);
            }

            saptamana.CurentMonday = DateTimeUtils.StartingDay;
            saptamana.IdPriza = idPriza;
            var rezervariPriza = _context.Rezervares
                .Where(p => p.IdPriza == idPriza && p.TimeIn.Date >= DateTimeUtils.StartingDay.Date && p.TimeIn.Date <= DateTimeUtils.StartingDay.Date.AddDays(7))
                .ToList();

            foreach (var x in rezervariPriza)
            {
                RezCalendar rezervare = new RezCalendar();
                rezervare.DataIn = x.TimeIn;
                rezervare.DataOut = x.TimeOut;
                rezervare.NrMasina = x.NrMasina;
                rezervare.ZiCurenta = x.TimeIn.DayOfWeek;
                saptamana.Week.Add(rezervare);

            }

            return View(saptamana);
        }


        /*
                public IActionResult PrevWeek(int idPriza)
                {
                    WeekCalendar saptamana = new WeekCalendar();
                    saptamana.Week = new List<RezCalendar>();


                    DateTime startingDay = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(-7);
                    var rezervariPriza = _context.Rezervares
                        .Where(p => p.IdPriza == idPriza && p.TimeIn.Date >= startingDay && p.TimeIn.Date <= startingDay.AddDays(7))
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


                public IActionResult NextWeek(int idPriza)
                {
                    WeekCalendar saptamana = new WeekCalendar();
                    saptamana.Week = new List<RezCalendar>();


                    DateTime startingDay = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7);
                    var rezervariPriza = _context.Rezervares
                        .Where(p => p.IdPriza == idPriza && p.TimeIn.Date >= startingDay && p.TimeIn.Date <= startingDay.AddDays(7))
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

        */

    }
}

