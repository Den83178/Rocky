using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using Rocky.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Rocky.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppinigCartList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart).Count > 0)                                                     // may IEnumerable
            {
                //session exists
                shoppinigCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppinigCartList.Select(i => i.ProductId).ToList();

            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));     // alt first or default

            return View(prodList);
        }


        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppinigCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart).Count > 0)                                                     // may IEnumerable
            {
                //session exists
                shoppinigCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppinigCartList.Remove(shoppinigCartList.FirstOrDefault(i => i.ProductId == id));

            //shoppinigCartList.RemoveAt(id);     it doesn't work!!!

            HttpContext.Session.Set<List<ShoppingCart>>(WC.SessionCart, shoppinigCartList);

            return RedirectToAction(nameof(Index));
        }

    }
}
