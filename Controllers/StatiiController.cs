using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Repositories;



namespace StatiiIncarcare.Controllers
{
    public class StatiiController : Controller
    {
        private readonly IncarcareStatiiContext _IncarcareStatiiContext;
        private readonly IStatiiRepository _statiiRepository;

        public StatiiController(IncarcareStatiiContext IncarcareStatiiContext, IStatiiRepository statiiRepository)
        {
            _IncarcareStatiiContext = IncarcareStatiiContext;
            _statiiRepository = statiiRepository;
        }


        public IActionResult GetStatii()
        {

            return View(_IncarcareStatiiContext.Statiis);
        }


        public IActionResult DeleteConfirmation(int id)
        {
            var statie = _IncarcareStatiiContext.Statiis.Where(p => p.IdStatie == id).FirstOrDefault();
            if (statie == null)
            {
                return NotFound();
            }
            return View("Delete", statie);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Statii statieDelete)
        {
            await _statiiRepository.Delete(statieDelete);
            return View("Thanks_delete", statieDelete);
        }

        public ActionResult Details(int id)
        {
            var statie = _IncarcareStatiiContext.Statiis
                .Include(x => x.Prizas)
                .ThenInclude(p => p.IdTipNavigation)
                .FirstOrDefault(x => x.IdStatie == id);

            if (statie == null)
            {
                return NotFound();
            }

            return View(statie);
            /*
             se poate mai eficient
            List<Statii> statiiList = _IncarcareStatiiContext.Statiis.ToList();
            List<Priza> prizeList = _IncarcareStatiiContext.Prizas.ToList();
            List<Tip> tipList = _IncarcareStatiiContext.Tips.ToList();

            var tipsRecord = from s in statiiList
                             join p in prizeList on s.IdStatie equals p.IdStatie
                             where s.IdStatie == id
                             select new ViewModel { StatieModel = s, PrizaModel = p };
                                                           

         
            return View(tipsRecord);
            */

            /* Old details version
             Statii model = new Statii()
              {
                  Nume = statie.Nume,
                  Oras = statie.Oras,
                  Adresa = statie.Adresa,
              };
              return View(model);
            */
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        // HTTP POST VERSION  
        [HttpPost]
        public async Task<IActionResult> Create(Statii statie)
        {
            await _statiiRepository.Create(statie);
            return View("Thanks_add", statie);
        }

        public ViewResult Filter(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nume" : "";
            ViewBag.OrasSortParm = String.IsNullOrEmpty(sortOrder) ? "Oras" : "";
            ViewBag.AdresaSortParm = String.IsNullOrEmpty(sortOrder) ? "Adresa" : "";
            var statii = from s in _IncarcareStatiiContext.Statiis
                         select s;


            switch (sortOrder)
            {
                case "Oras":
                    statii = statii.OrderBy(s => s.Oras);
                    break;
                case "Nume":
                    statii = statii.OrderBy(s => s.Nume);
                    break;
                case "Adresa":
                    statii = statii.OrderBy(s => s.Adresa);
                    break;
                default:
                    statii = statii.OrderBy(s => s.Oras);
                    break;
            }

            return View("GetStatii",statii);
        }
    }
}
