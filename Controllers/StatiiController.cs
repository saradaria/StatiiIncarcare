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
            return View("Delete",statie);
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
            var statii= from s in _IncarcareStatiiContext.Statiis
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

            return View(statii.ToList());
        }
    }
}



/*
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(int id, [Bind("IdStatie,Nume,Oras,Adresa")] Statii statie)
{

    if (id != statie.IdStatie)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _IncarcareStatiiContext.Update(statie);
            await _IncarcareStatiiContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StatiiExist(statie.IdStatie))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(statie);
}

private bool StatiiExist(int idStatie)
{
    throw new NotImplementedException();
}
}
}


  public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
{
    if (id == null)
    {
        return NotFound();
    }

    var statie = await _IncarcareStatiiContext.Statiis
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.IdStatie == id);
    if (statie == null)
    {
        return NotFound();
    }

    if (saveChangesError.GetValueOrDefault())
    {
        ViewData["ErrorMessage"] =
            "Delete failed. Try again, and if the problem persists " +
            "see your system administrator.";
    }

    return View(statie);
}
*/

