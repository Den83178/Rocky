using Microsoft.AspNetCore.Mvc;
using Rocky.Models;
using Rocky.Data;


namespace Rocky.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _dbat;
        public ApplicationTypeController(ApplicationDbContext db_at, ApplicationDbContext db_c)
        {
            _dbat = db_at;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objat = _dbat.ApplicationType;
            return View(objat);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType at_string)
        {
            if (ModelState.IsValid)
            {
                _dbat.ApplicationType.Add(at_string);
                _dbat.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Get - Edit
        public IActionResult Edit(int? id_1)
        {
            if(id_1==0 || id_1==null)
            {
                return NotFound();
            }
                var ob = _dbat.ApplicationType.Find(id_1);
            if(ob==null)
            {
                return NotFound();
            }
                return View(ob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType edit)
        {
            if (ModelState.IsValid)
            {
                _dbat.ApplicationType.Update(edit);
                _dbat.SaveChanges();
                return RedirectToAction("Index");          
            }
            return View();
        }

        //GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == 0|| id == null)
            {
                return NotFound();
            }
            var del = _dbat.ApplicationType.Find(id);
            if(del==null)
            {
                return NotFound();
            }
            return View(del);
        }

        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete1(int? id)
        {
            var ob_del = _dbat.ApplicationType.Find(id);
            if (ob_del == null)
            {
                return NotFound();
            }
                _dbat.ApplicationType.Remove(ob_del);
                _dbat.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
