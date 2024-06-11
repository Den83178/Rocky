using Microsoft.AspNetCore.Mvc;

namespace Rocky.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
