using System.Linq;
using System.Web.Mvc;
using MakersMarket.Attributes;
using MakersMarket.Models;
using MakersMarket.Models.Domain;
using MakersMarket.Models.View.Cart;

namespace MakersMarket.Areas.Store.Controllers
{
    public class CartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var cart = GetCart();

            var viewModel = new CartIndexViewModel
            {
                CartItems = cart.Items()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ID")] Product product)
        {
            var addedItem = db.Products
                .Single(p => p.ID == product.ID);
            var cart = GetCart();
            cart.Add(addedItem);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpParamAction]
        public ActionResult Remove([Bind(Include = "ID")] Product product)
        {
            var cart = GetCart();
            cart.Remove(product.ID);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpParamAction] 
        public ActionResult Clear()
        {
            var cart = GetCart();
            cart.Clear();

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult Items()
        {
            var cart = GetCart();
            ViewData["ItemsCount"] = cart.ItemsCount();

            return PartialView("_Items");
        }

        private Cart GetCart()
        {
            if (Request.IsAuthenticated)
            {
                return Cart.GetCart(User.Identity.Name);
            }
            else
            {
               return Cart.GetCart(this.HttpContext);
            }
        }
    }
}