using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace StatiiIncarcare.Controllers
{
    public class ChartController : Controller
    {
        private readonly IncarcareStatiiContext _context;
		List<Point> dataPoints = new List<Point>();

		public ChartController(IncarcareStatiiContext context)
        {
            _context = context;
        }

		public ActionResult Index()
			{
			foreach (var statie in _context.Statiis)
			{
				var id = statie.IdStatie;
				var count = _context.Statiis
				.Include(x => x.Prizas)
				.ThenInclude(p => p.Rezervares)
				.Where(x => x.IdStatie == id).Count();
				
				Point newPoint = new Point(statie.Nume, count);
				dataPoints.Add(newPoint);
			}

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

				return View();
			}
		}
	}     