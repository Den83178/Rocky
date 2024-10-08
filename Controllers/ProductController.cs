using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext prod)
        {
            _db = prod;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> ob_prod = _db.Product;

            foreach (var ob_cat in ob_prod)
            {
                ob_cat.Category = _db.Category.FirstOrDefault(u => u.Id==ob_cat.CategoryId);
            }
            return View(ob_prod);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                // this is for create
                return View(product);
            }
            else
            {
                product = _db.Product.Find(id);

                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }
    }
}
