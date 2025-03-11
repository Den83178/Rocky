using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using Rocky.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Rocky.Models.ViewModels;

namespace Rocky.Controllers
{
    [Authorize]                                     //Microsoft.ASPNetCore.Authorization
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

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

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        
        public IActionResult IndexPost()
        {
            

            return RedirectToAction(nameof(Summary));
        }


        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = User.FindFirstValue(ClaimTypes.Name);

            List<ShoppingCart> shoppinigCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart).Count > 0)                                                     // may IEnumerable
            {
                //session exists
                shoppinigCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppinigCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));     // alt first or default

            ProductUserVM = new ProductUserVM() 
            {
               ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value)
            };

            return View(nameof(ProductUserVM));
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
