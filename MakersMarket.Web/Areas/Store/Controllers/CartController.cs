namespace MakersMarket.Web.Areas.Store.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Data;
    using Attributes;
    using MakersMarket.Models;
    using Models.Cart;
    using Web.Controllers;
    using Web.Models.Cart;
    public class CartController : BaseController
    {
        public CartController(IMakersMarketData data)
            : base(data)
        {
        }
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
            var addedItem = this.Data.Products.All()
                .Single(p => p.Id == product.Id);
            var image = this.Data.Images.All().Where(i => i.ProductId == product.Id).ToList();
            addedItem.Images = image;
           
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
            cart.Remove(product.Id);

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